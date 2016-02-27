using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Text;
using System;

using System.IO.Ports;


public class HBListener : MonoBehaviour {
	//listen for heartbeat data

	//public int UDP_LISTEN_PORT = 50000;
	//private UdpClient client;
	//private IPEndPoint remoteEP;
	//private bool messageReceived = true;

	private CMS50Dplus listener;


	void Start() {
		//foreach (string s in SerialPort.GetPortNames()) print(s);
		//Debug.Log("Starting Client");
		//remoteEP = new IPEndPoint(IPAddress.Any, UDP_LISTEN_PORT);
		//client = new UdpClient(remoteEP);
		listener = new CMS50Dplus("COM4");
		StartCoroutine(listener.getLiveData());
		StartCoroutine(receiveData());
	}

	IEnumerator receiveData() {
		print("reading");
		
		//IEnumerator reader = listener.getLiveData();
		LiveDataPoint point = null;
		while (true) {
			
			//while (reader.MoveNext()) {
			/*while (reader.MoveNext()) {
				print("ayy");
				point = reader.Current as LiveDataPoint;
				if (point != null) {// && Mathf.Abs((DateTime.Now - point.time).Seconds) <= 1) {
					print("timediff: " + Mathf.Abs((DateTime.Now - point.time).Seconds));
					break;
				}
				
				//if (point != null) print("fingerOut: " + point.fingerOut + " pulseRate: " + point.pulseRate + " time: " + point.time);
				yield return new WaitForSeconds(.01f);
			}
			print("tick");*/
			//point = reader.Current as LiveDataPoint;
			point = listener.latest;
			print("tick: " + DateTime.Now);
			if (point != null) print("fingerOut: " + point.fingerOut + " pulseRate: " + point.pulseRate + " time: " + point.time);
			else print("disconnect");
			yield return new WaitForSeconds(1f);
		}
	}

		/*IEnumerator receiveData() {
			while (true) {
				if (messageReceived) {
					messageReceived = false;
					print("Waiting for broadcast");
					client.BeginReceive(new AsyncCallback(_receiveData), null);
				}

				//byte[] receivedBytes = client.Receive(ref remoteEP);

				yield return new WaitForSeconds(1);
			}
		}

		void _receiveData(IAsyncResult result) {
			byte[] receivedBytes = client.EndReceive(result, ref remoteEP);
			print("received: " + Encoding.ASCII.GetString(receivedBytes));
			messageReceived = true;
		}*/
	}
