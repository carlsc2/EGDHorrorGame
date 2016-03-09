using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Text;
using System;

using System.IO.Ports;


public class HBListener : Singleton<HBListener> {
	//listen for heartbeat data

	private CMS50Dplus listener;

	public int measurements_per_second = 10;
	public float time_window = 5;//time window in seconds to take average
	private int windowsize = 50; 
	private int[] dataBuffer; //buffer for average heartbeats
	private int index = 0;
	public int avgPulse = -1;
	public int outPulse = -1;
	public string port = "COM3";

	void calc_avg() {
		int sum = 0;
		for(int i=0; i<windowsize; i++) {
			sum += dataBuffer[i];
		}
		avgPulse = sum / windowsize;

	}

	void Start() {
		windowsize = (int)(time_window * measurements_per_second);
		dataBuffer = new int[windowsize];
		//foreach (string s in SerialPort.GetPortNames()) print(s);
		listener = new CMS50Dplus(port);
		StartCoroutine(listener.getLiveData());
		StartCoroutine(receiveData());
	}

	IEnumerator receiveData() {
		print("reading");
		
		LiveDataPoint point = null;
		while (true) {
			point = listener.latest;

			print("index: " + index + " tick: " + DateTime.Now);

			if (point != null) {
				if (point.fingerOut || point.pulseRate == 0) {
					outPulse = -1;
					avgPulse = -1;
				}
				else {
					index = ++index % windowsize;
					dataBuffer[index] = point.pulseRate;
					print("fingerOut: " + point.fingerOut + " pulseRate: " + point.pulseRate + " time: " + point.time);
					outPulse = point.pulseRate;
					calc_avg();
					print("average: " + avgPulse);
				}
			}
			else {
				print("disconnect");
			}			
			yield return new WaitForSeconds(1.0f/measurements_per_second);
		}
	}


	new void OnDestroy() {
		base.OnDestroy();
		//close serial port
		listener.disconnect();
		print("disconnected from port: " + port);
	}
}
