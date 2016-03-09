using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Text;
using System;

using System.IO.Ports;


public class HBListener : MonoBehaviour {
	//listen for heartbeat data

	private CMS50Dplus listener;

	public int windowsize = 50; //look at last N measurements
	private int[] dataBuffer; //buffer for average heartbeats
	private int index = 0;
	public int avgPulse = 0;
	public int outPulse;
    public string port = "COM3";

	void calc_avg() {
		int sum = 0;
		for(int i=0; i<windowsize; i++) {
			sum += dataBuffer[i];
		}
		avgPulse = sum / windowsize;

	}

	void Start() {
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
			if (point != null) {
				index = ++index % windowsize;
				dataBuffer[index] = point.pulseRate;
			}
			print("index: " + index);
			calc_avg();
			print("tick: " + DateTime.Now);
			print("average: " + avgPulse);

            if (point != null && !listener.fingerOut){
                print("fingerOut: " + point.fingerOut + " pulseRate: " + point.pulseRate + " time: " + point.time);
                outPulse = point.pulseRate;
            }
            else print("disconnect");
			yield return new WaitForSeconds(.1f);
		}
	}


	void OnDestroy() {
		//close serial port
		listener.disconnect();
	}
}
