using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

using System.IO.Ports;


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

	private bool calibrating = false;

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
		//foreach (string s in SerialPort.GetPortNames()) print(s);
		listener = new CMS50Dplus(port);
		StartCoroutine(listener.getLiveData());
		StartCoroutine(receiveData());
		recalibrate();
	}

	public void recalibrate() {
		if(!calibrating) StartCoroutine(calibration());
	}

	IEnumerator receiveData() {
		print("reading");

		LiveDataPoint point = null;
		while (true) {
			point = listener.latest;

			//print("index: " + index + " tick: " + DateTime.Now);

			if (point != null) {
				connected = true;
				if (point.fingerOut || point.pulseRate == 0) {
					outPulse = -1;
					avgPulse = -1;
					ticks = 0;
				}
				else {
					ticks += 1;
					index = ++index % windowsize;
					dataBuffer[index] = point.pulseRate;
					//print("fingerOut: " + point.fingerOut + " pulseRate: " + point.pulseRate + " time: " + point.time);
					outPulse = point.pulseRate;
					if (ticks > windowsize) {
						calc_avg();
					}
					//print("average: " + avgPulse);
				}
			}
			else {
				//print("disconnect");
				connected = false;
			}			
			yield return new WaitForSeconds(1.0f / measurements_per_second);
		}
	}

	IEnumerator calibration() {
		calibrating = true;
		while (!connected || outPulse==-1) {
			yield return null;
		}
		print("Calibration start");
		List<int> calibration_buffer = new List<int>();
		float start_time = Time.time;
		while (Time.time - start_time < calibration_time) {
			calibration_buffer.Add(outPulse);
			yield return new WaitForSeconds(1.0f / measurements_per_second);
		}
		int sum = 0;
		foreach (int val in calibration_buffer) {
			sum += val;
		}
		base_rate = sum / calibration_buffer.Count;
		print("Baseline calibrated: " + base_rate);
		calibrating = false;

	}


	new void OnDestroy() {
		base.OnDestroy();
		//close serial port
		listener.disconnect();
		print("disconnected from port: " + port);
	}
}
