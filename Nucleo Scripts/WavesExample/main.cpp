#include "mbed.h"


Serial pc(USBTX,USBRX);
DigitalOut Vcc(A0);
AnalogIn ain(A1);
Timer timer;
double resistori = 11930;

int main()
{
	
	Vcc = 0;
	wait(20);
	
	timer.start();
	double aika = 0;
	double adMitattu = 1;

	
	while(true)
    {	
		Vcc = 1;
		wait(20);	// Odotetaan että kondensaattori on täysin varautunut
		
		Vcc = 0;
		
		// Mitataan aika, joka kuluu, kun varautunut kondseettaro purkautuu 37% täydestä arvostaan
		timer.reset();
		while(adMitattu > 0.37)
		{
			adMitattu = ain.read();
		}
		aika = timer.read_ms();
		
		double kapasitanssi = (aika/1000) / resistori;		
		pc.printf("%.2lfuF %.0fms\n\n", kapasitanssi*1000000, aika);
		adMitattu = 1;
	}
}

/* KAPASITANSSI VARAUKSELLA LASKETTUNA
// Syötetään 5v jännite
		Vcc = 1;	
	
		// Mitataan aika joka kuluu, kun mitattu jännite nousee 63% lopullisesta arvosta
		timer.reset();
		while(adMitattu < 0.63)
		{	
			adMitattu = ain.read();
		}
		aika = timer.read_ms();  
		Vcc = 0;
		
		double kapasitanssi = (aika/1000) / resistori;		
		pc.printf("%.2lfuF %.0fms\n\n", kapasitanssi*1000000, aika);
		adMitattu = 0;
		
		// Odotellaan varauksen purkautumista...
		wait(20);
*/                                          