#include "mbed.h"
#include "L3G4200D.h"
#define M_PI 3.14159

Serial pc(USBTX,USBRX);
L3G4200D gyro(D14, D15);
Timer timer;

DigitalOut Vcc(A0);
AnalogIn xIn(A1);
AnalogIn yIn(A2);
AnalogIn zIn(A3);
DigitalOut Gnd(A4);

int g[3] = {0,0,0};


int main()
{
	Vcc = 1;
	Gnd = 0;
	timer.start();
	
	float roll = 0;
	float pitch = 0;
	float gyro_roll = 0;
	float gyro_pitch = 0;
	
	while(true)
    {
		float aika = timer.read_ms();        
		timer.reset();
		
		gyro.read(g);
		
		g[0] = g[0] - 4;
		g[1] = g[1] + 6;
		g[2] = g[2] - 6;

		float ax = 98.585 * xIn.read() - 48.307;
		float ay = 95.243 * yIn.read() - 48.288;
		float az = 101.1 * zIn.read() - 51.056;
				
		gyro_pitch += g[1] * (aika / 1000) * 0.070054;
		gyro_roll += g[0] * (aika / 1000) * 0.070054;
				
		float acc_pitch = 180 * atan (ax/sqrt(ay*ay + az*az))/M_PI;
		float acc_roll = 180 * atan (ay/sqrt(ax*ax + az*az))/M_PI;
		
				
		pitch = 0.8 * (pitch + gyro_pitch * aika/1000) + 0.2 * acc_pitch;
	    roll = 0.8 * (roll + gyro_roll * aika/1000) + 0.2 * acc_roll;
				
		pc.printf("%f %f \n", pitch, roll);

	}
}
