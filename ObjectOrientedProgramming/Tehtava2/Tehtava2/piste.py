class Piste:
    """ Luokka, jonka kaikki oliot tietävät x ja y koordinaatit """

    def __init__(self, _x=0, _y=0):
        self.__x = _x
        self.__y = _y

    #olion metodit
    def pituus(self):
        """ pisteen etäisyys origosta """
        return pow((self.__x**2 + self.__y**2), 0.5)
    def etaisyys(self, _piste):
        """ pisteen etäisyyden annetusta pisteesta """
        return pow(((self.__x - _piste.__x)**2 + (self.__y - _piste.__y)**2), 0.5)
    def koordinaatit(self):
        return self.__x, self.__y
    def koordinaattix(self):
        """ palauttaa koordinaatin x arvon """
        return self.__x
    def koordinaattiy(self):
        """ palauttaa koordinaatin y arvon """
        return self.__y

