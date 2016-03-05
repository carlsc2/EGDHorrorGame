﻿using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Text;
using System;

using System.IO.Ports;


public class HBListener : MonoBehaviour {
	//listen for heartbeat data

	private CMS50Dplus listener;

	void Start() {
		//foreach (string s in SerialPort.GetPortNames()) print(s);
		listener = new CMS50Dplus("COM4");
		StartCoroutine(listener.getLiveData());
		StartCoroutine(receiveData());
	}

	IEnumerator receiveData() {
		print("reading");
		
		LiveDataPoint point = null;
		while (true) {
			point = listener.latest;
			print("tick: " + DateTime.Now);
			if (point != null && !listener.fingerOut) print("fingerOut: " + point.fingerOut + " pulseRate: " + point.pulseRate + " time: " + point.time);
			else print("disconnect");
			yield return new WaitForSeconds(.1f);
		}
	}


	void OnDestroy() {
		//close serial port
		listener.disconnect();
	}
}
