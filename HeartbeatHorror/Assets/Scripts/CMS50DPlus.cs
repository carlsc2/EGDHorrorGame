using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;

public class CMS50Dplus {

	private string port;
	private SerialPort conn;
	public LiveDataPoint latest;

	public CMS50Dplus(string port) {
		this.port = port;
	}

	public bool isConnected() {
		return conn != null && conn.IsOpen;
	}

	public void connect() {
		if (conn == null) {
			int baudrate = 19200;
			conn = new SerialPort(port, baudrate, Parity.Odd, 8, StopBits.One);
			conn.ReadTimeout = 5;
			conn.Handshake = Handshake.XOnXOff;
			conn.Open();
		}
		else if (!isConnected()) {
			conn.Open();
		}

	}

	public void disconnect() {
		if (isConnected()) {
			conn.Close();
		}
	}

	public IEnumerator getLiveData() {
		connect();
		while (true) {
			byte[] packet = { 0, 0, 0, 0, 0 };
			bool timeout = false;
			try {
				conn.Read(packet, 0, 5);
			}catch (TimeoutException){
				//Debug.Log("timeout");
				timeout = true;
			}
			if (timeout) {
				//latest = null;
				yield return null;
			}
			else {
				if (Convert.ToBoolean(packet[0] & 0x80)) {
					//yield return new LiveDataPoint(DateTime.Now, packet);
					latest = new LiveDataPoint(DateTime.Now, packet);
				}
			}	
			yield return null;
		}
	}
}

public class LiveDataPoint {
	public DateTime time;
	public int signalStrength;
	public bool fingerOut;
	public bool droppingSpO2;
	public bool beep;
	public int pulseWaveform;
	public int barGraph;
	public bool probeError;
	public bool searching;
	public int pulseRate;
	public int bloodSpO2;


	public LiveDataPoint(DateTime time, byte[] data) {
		if (
			(((data[0] & 0x80) != 0) != true) &&
			(((data[0] & 0x80) != 0) != false) &&
			(((data[0] & 0x80) != 0) != false) &&
			(((data[0] & 0x80) != 0) != false) &&
			(((data[0] & 0x80) != 0) != false)
			) throw new Exception("Invalid data packet.");

		this.time = time;

		// 1st byte
		signalStrength = data[0] & 0x0f;
		fingerOut = Convert.ToBoolean(data[0] & 0x10);
		droppingSpO2 = Convert.ToBoolean(data[0] & 0x20);
		beep = Convert.ToBoolean(data[0] & 0x40);

		// 2nd byte
		pulseWaveform = data[1];

		// 3rd byte
		barGraph = data[2] & 0x0f;
		probeError = Convert.ToBoolean(data[2] & 0x10);
		searching = Convert.ToBoolean(data[2] & 0x20);
		pulseRate = (data[2] & 0x40) << 1;

		// 4th byte
		pulseRate |= data[3] & 0x7f;

		// 5th byte
		bloodSpO2 = data[4] & 0x7f;
	}
}