from piste import Piste

import math

class Kolmio:
    """Luokka, joka tietää kolme Pistettä (Piste-luokka)"""

    def __init__(self, _p1, _p2, _p3):
        self.__p1 = _p1
        self.__p2 = _p2
        self.__p3 = _p3

        #olion metodit

    def piiri(self):
        """ kolmion piirin laskeminen """
        # lasketaan kolmion sivujen pituudet
        self.sivu1 = self.__p1.etaisyys(self.__p2)
        self.sivu2 = self.__p2.etaisyys(self.__p3)
        self.sivu3 = self.__p3.etaisyys(self.__p1)
        # palautetaan sivut yhteensä
        return self.sivu1 + self.sivu2 + self.sivu3

    def ala(self):
        """ pinta-alan laskenta """
        # lasketaan p heronin kaavaan
        p = self.piiri() / 2
        # palautetaan kolmion pinta-ala heronin kaavaa käyttäen
        return (math.sqrt (p * (p - self.sivu1) * (p - self.sivu2) * (p - self.sivu3)))

