# Controllers

The ```Controllers``` folder contains the controllers that our api uses to accept incoming requests and send outgoing responses. 

We will aim for as skinny controllers as we can possibly have. With the help of the mediator pattern and the MediatR library we should be able to make it so our controllers have only the IMediator object injected into their constructors.

Unit tests for any controller should cover all the ResponseTypes that controller could possibly return.

Also, we should unit test the controllers to make sure the actual url endpoint doesn't change at any point.

> Renaming a controller shouldn't alter the endpoint that we are already exposing to the world!