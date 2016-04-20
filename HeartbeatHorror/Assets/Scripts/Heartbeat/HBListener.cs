using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HBListener : Singleton<HBListener> {
	//listen for heartbeat data

	private CMS50Dplus listener;

	public int measurements_per_second = 10;
	public float avg_window = 5;//time window in seconds to take current average
	public float calibration_time = 15;//time window to calibrate base rate
	private int windowsize = 50; 
	private int[] dataBuffer; //buffer for average heartbeats
	private int index = 0;
	private int ticks = 0;
	public int avgPulse = -1;
	public int outPulse = -1;
	public string port = "COM3";
	public bool connected = false;
	public int base_rate = -1; //baseline heart rate

	public float calibration_interval = 300;//every X seconds, recalibrate the base rate
	private float last_calibration_time = -1000;
	public bool calibrated = false;

	public bool test_mode = false;

	void calc_avg() {
		int sum = 0;
		for(int i=0; i<windowsize; i++) {
			sum += dataBuffer[i];
		}
		avgPulse = sum / windowsize;
	}

	void Start() {
		avgPulse = outPulse = -1;
		windowsize = (int)(avg_window * measurements_per_second);
		dataBuffer = new int[windowsize];
		listener = new CMS50Dplus(port);
		StartCoroutine(listener.getLiveData());
		StartCoroutine(receiveData());
		StartCoroutine(calibration());

		if (test_mode) {
			avgPulse = 80;
			base_rate = 70;
		}
	}

	IEnumerator receiveData() {
		print("reading");
		LiveDataPoint point = null;
		while (true) {
			point = listener.latest;
			if (point != null) {
				connected = true;
				if (point.fingerOut || point.pulseRate == 0) {
					//outPulse = -1;
					//avgPulse = -1;
					ticks = 0;
				}
				else {
					ticks += 1;
					index = ++index % windowsize;
					dataBuffer[index] = point.pulseRate;
					outPulse = point.pulseRate;
					if (ticks > windowsize) {
						calc_avg();
					}
				}
			}
			else {
				connected = false;
			}			
			yield return new WaitForSeconds(1.0f / measurements_per_second);
		}
	}

	IEnumerator calibration() {
		while (true) {
			calibrated = true;
			while (!connected || outPulse == -1) {//wait for a connection to be established before calibrating
				yield return null;
			}
			print("Calibration start");
			bool interrupted = false;
			List<int> calibration_buffer = new List<int>();
			float start_time = Time.time;
			while (Time.time - start_time < calibration_time) {
				if(!connected || outPulse == -1) {//reset calibration if interrupted
					print("Calibration interrupted");
					interrupted = true;
					break;
				}
				calibration_buffer.Add(outPulse);
				yield return new WaitForSeconds(1.0f / measurements_per_second);
			}
			if (interrupted) {
				continue;
			}
			int sum = 0;
			foreach (int val in calibration_buffer) {
				sum += val;
			}
			base_rate = sum / calibration_buffer.Count;
			print("Baseline calibrated: " + base_rate);
			calibrated = false;
			last_calibration_time = Time.time;
			//wait for interval before recalibrating or start immediately if interrupted while waiting
			while (Time.time - last_calibration_time < calibration_interval) {
				if (!connected || outPulse == -1 || base_rate / avgPulse > 1.15f) {//recalibrate if interrupted or if current is 15% below baseline
					break;
				}
				yield return new WaitForSeconds(1);
			}
			
		}
	}


	new void OnDestroy() {
		base.OnDestroy();
		//close serial port
		listener.disconnect();
		print("disconnecting from port: " + port);
	}
}
