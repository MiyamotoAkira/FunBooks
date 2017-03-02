# FunBooks

This is an exercise based on [David Thomas' Kata 16](http://codekata.com/kata/kata16-business-rules/)

## The initial idea

Clearly to eliminate the need to have to change the code constantly a way to provide the ability to create rules
from outside the code is needed.     

I thought about two possible solutions: A proper full DSL that gives flexibility to create completely arbitrary
rules and actions, or a more limited set, which will be just indication of calls to the public API available,
with minimal flexibility.

I don't believe that C# is the right tool for the first type, so I went ahead with the second option. To codify the rules
YAML to codify the rules. Especially as the Purchase Order does look already like YAML. For this to work in a production
environment, a comprehensive way to be able to do the actions is required through the API. But for the purposes of this
exercise, so far, I have only provided the methods that will work with the business rules that have been provided as examples.
IPOModifer will provide the methods that will need to be implemented.

## Some considerations
At the moment, there are a few interfaces that do not have implementation. It was not needed to cover the requirements.
The application uses netstandard 1.6 (netcore 1.1). Haven't tested the application outside of it (see next section).
Code is not production ready (check ToDo section). I think it does give an idea of where I am going from an architectural
point of view.

## Issues so far
The main issue found has been around the technology used. Because I had started this on my holidays, I only had my 
Linux laptop available. Therefore, it was time to learn how to use .Net Core. Which has been interesting to say the least. 
Even more, as there are libraries only present on a prerelease form (Moq) or not present at all (any YAML library), I have
spent more time trying to setup basic stuff rather than the solution itself. 

## ToDo
- Try to store variables from the rules to be used as parameters on the actions.
- Refactor Apply method (can it be extracted to a text file?)
- Add defensive code
- Create Action object same as Rule object
- Refactor the parsing of rules and actions.
- Some cleaning of code to be done.
- PurchaseOrder Constructor to be converted to builder pattern?