# Lab 3: Mini API Project

## GET Calls

### GET all persons

Returns first names, last names and phone numbers of persons in database.

https://localhost:xxxx/persons

### GET all interests

Returns names and descriptions of all interests.

https://localhost:xxxx/interests

### GET all links

Returns all URLs in the database

https://localhost:xxxx/links

### GET all links connected to a specific person's interest

https://localhost:xxxx/persons/1/interests/links

### GET all interests connected to a specific person

https://localhost:xxxx/persons1/interests

## POST Calls

### POST new interest to a specific person

https://localhost:xxxx/persons/1/interest

### POST new link connected to a specific person's interest

https://localhost:xxxx/persons/1/interests/1/links

### POST existing interest to an existing person

https://localhost:xxxx/persons/1/interests/1

## ER Diagram and Class UML

![MiniApiER](https://github.com/seanie1995/Lab-3-Mini-API/assets/119659530/b0fffb94-6fca-4440-9c43-44b2103c90fc)



![MiniApiUML drawio(2)](https://github.com/seanie1995/Lab-3-Mini-API/assets/119659530/0be67761-eeeb-44e3-95e0-827b898cf68a)


