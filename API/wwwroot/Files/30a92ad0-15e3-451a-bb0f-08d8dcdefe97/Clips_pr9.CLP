(clear)
(defglobal ?*t* = 0)
(deftemplate spivrobitnyk
	(slot lastname)
	(slot staj(type INTEGER))
	(slot children)
	(slot money)
)
(deftemplate premia
	(slot lastname)
	(slot suma)
)
=>
(deffacts personal
	(spivrobitnyk (lastname "L1")(staj 21)(children 2)(money 5000))
	(spivrobitnyk (lastname "L2")(staj 6) (children 3)(money 3000))
	(spivrobitnyk (lastname "L3")(staj 5) (children 1)(money 7000))
	(spivrobitnyk (lastname "L4")(staj 8) (children 0)(money 6000))
	(spivrobitnyk (lastname "L5")(staj 7) (children 2)(money 4000)) )
(reset)

(defrule r1 
	(spivrobitnyk (lastname ?lastname)(staj ?staj)(children ?children)(money ?money))
	(test(or( >= ?staj 5)(> ?children 2)))
		
=>
	(assert (premia (lastname ?lastname)(suma (* ?money 0.2))))
	(bind ?*t* (+ ?*t* 1))
)

(run)
(defrule r2
	(test(= ?*t* 5))
	
=>
(printout t "All" crlf)
)
(defrule r3
	(spivrobitnyk (lastname ?lastname)(staj ?staj)(children ?children)(money ?money))
	(exists( test(>= ?staj 20)))
		
=>
	(printout t "Veteran exists" crlf)
)
(run)
(facts)
