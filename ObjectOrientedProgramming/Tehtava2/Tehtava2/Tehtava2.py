from piste import Piste
from kolmio import Kolmio

import math

def main():
    """analogous to other programming languages
        in Python has no special role
        """
    #nimetty olio, muodostettu ilman parametreja
    _p_1 = Piste()
    #kutsutaan nimetyn olion palvelua ja palautusarvo asetetaan muuttujan arvoksi
    _len_1 = _p_1.pituus()
    print("pisteen 1 etäisyys origosta ", _len_1)
    koord1 = _p_1.koordinaatit()
    #nimetty olio, muodostimelle annettu parametrit
    _p_2 = Piste(1, 2)
    #kutsutaan nimetyn olion palvelua ja palautusarvo asetetaan muuttujan arvoksi
    _len_2 = _p_2.pituus()
    print("pisteen 2 etäisyys origosta ", _len_2)
    koord2 = _p_2.koordinaatit()
    #nimetty olio, muodostimelle annettu parametrit
    _p_3 = Piste(4,5)
    #kutsutaan nimetyn olion palvelua ja palautusarvo asetetaan muuttujan arvoksi
    _len_3 = _p_3.pituus()
    print("pisteen 3 etäisyys origosta ", _len_3)
    koord3 = _p_3.koordinaatit()
    #nimetty olio, muodostimelle annettu parametrit
    _p_4=Piste(3,4)
    #kutsutaan nimetyn olion palvelua ja palautusarvo asetetaan muuttujan arvoksi
    _len_4 = _p_4.pituus()
    print("pisteen 4 etäisyys origosta ", _len_4)
    koord4 = _p_4.koordinaatit()
    # Tulostaa pisteiden koordinaatit
    print ('pisteiden 1, 2, 3 ja 4 koordinaatit ovat:', koord1, koord2, koord3, koord4)
    #lasketaan kahden annetun pisteen välinen etäisyys
    x2 = _p_2.koordinaattix()
    x4 = _p_4.koordinaattix()
    y2 = _p_2.koordinaattiy()
    y4 = _p_4.koordinaattiy()
    dist = round(math.sqrt((x4-x2)**2 + (y4-y2)**2),2)
    print ('Pisteiden 2 ja 4 välinen etäisyys toisistaan on ', dist)
    print (_p_4.etaisyys(_p_3))
    #Suoritetaan funktio pisteillä 2 ja 4
    kahden_pisteen_valinen_etaisyys(_p_2, _p_4)
    #Suoritetaan funktio pisteillä 2, 3 ja 4
    kolmion_ala_ja_piiri(_p_2, _p_3, _p_4)
       

def kahden_pisteen_valinen_etaisyys(_p_1, _p_2):
    _p = _p_1.koordinaatit()
    _p2 = _p_2.koordinaatit()
    #Tulostaa pisteiden x ja y arvot
    print ('piste 1 x:', _p[0],', piste 1 y:', _p[1],', piste 2 x:', _p2[0],', piste 2 y:', _p2[1])
    #Laskee kahden pisteen etäysyyden
    tulos = pow(((_p[0] - _p2[0])**2 + (_p[1] - _p2[1])**2), 0.5)
    print ('Pisteiden välinen etäisyys on', tulos)
    return tulos

def kolmion_ala_ja_piiri(_p_1, _p_2, _p_3):
    #Luodaan kolmio
    _k = Kolmio(_p_1, _p_2, _p_3)
    # Tulostetaan kolmion piiri ja pinta-ala
    tulostus = ('Kolmion piirin pituus on ', _k.piiri(), ' ja pinta-ala on ', _k.ala())
    print (tulostus)
    return tulostus

    


# execute main() if the program is launched as a script,
# but not if it is imported as a module
if __name__ == "__main__":
    main()
