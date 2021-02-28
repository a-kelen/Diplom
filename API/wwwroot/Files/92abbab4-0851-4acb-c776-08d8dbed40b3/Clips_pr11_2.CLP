(clear)

(deffacts mans
	(man Ivan Vasyl Petro )
	(man Petro Ivan Vasyl )
	(man Mykola Vasyl Ivan)
	(man Vasyl Petro Vasyl)
)
(reset)
(assert (proof Petro F) )
(assert (proof Vasyl T )

(defrule toClaim
	(man (name ?n)(m1 ?m1)(m2 ?m2))
=>
	(assert (claim ?m1))
	(assert (claim ?m2))
)
(defrule detect
	(proof (name ?n)( state ?s))
	(man (name ?n)(m1 ?m1)(m2 ?m2))
	(exists (claim (name ?m1))) 
	(exists (claim (name ?m2))) 
	
=>
	
	(if (eq ?s F)
		then
		(assert (torem ?m1 R))
		(assert (torem ?m2 R))
	)				
)
(defrule remClaim 
	(torem ?name ?st)
	?f1 <- (claim (name ?name))
=>
	(retract ?f1)
)

(defrule detectTruph 
	(man (name ?n)(m1 ?m1)(m2 ?m2))
 	(exists (claim (name ?m1)))
	(exists (claim (name ?m2))) 
=>
	(printout t "True - " ?n " " ?m1 ?m2 crlf)
	(assert (proof (name ?n)(state T)))
)
(defrule detectLie
	(man (name ?n)(m1 ?m1)(m2 ?m2))
 	
=>
	(printout t ?n " " ?m1 ?m2 crlf)
	
)

(run)
(facts)