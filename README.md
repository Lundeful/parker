# Parker

A mock project created to demonstrate how I like to structure my new projects and trying to strike a balance of developer velocity with code
quality and complexity. It enables rapid development while maintaining a good enough structure to avoid technical debt as the
project grows. It supports both simple CRUD features, and more complex scenarios. This is just my personal preference. I'm not claiming it
should be the default for everyone, but I love it!

This is a minimal example that shows how I could start out a new project. Important stuff like auth and validation is completely skipped for
demo purposes. Only use it for inspiration on what to do (or not to do, if you hate it). If you read through this doc and code and still
have some questions on how I solve specific things, you can open up a new thread on the "discussions" tab and tag me. There are likely a lot
of cases I have dealt with that are not represented here.

PS: This word salad of a README is human-generated slop, not AI-slop, and English is not my native language. I will not rewrite it with AI.
I'll manually update the readme with something if you ask me to.

## The Domain

The idea for the domain is a backend for a parking garage system. To keep it simple, I just started with two feature sets/domain concepts;
Parking spots and parking sessions. One could imagine this is a backend for either manual frontend (app or parking meter) or automated
solution (cameras?).

Current features:

* Add a new parking spot (Need to have at least one to start a session)
* Get all parking spots
* Start a parking session
* Stop a specific parking session
* Get recently completed parking sessions

**Note:** I just chose a random domain for fun, so my assumptions on how the domain works might be *way* off. But given my structure, it
should be easy to change behavior and add/remove features if I were to make this a real project. Which is kind of the point! :)

## The core philosophy

It's my personal hybrid of Vertical Slice Architecture, Clean Architecture, general "pragmatic" development and Domain Driven Design (DDD).
Jumping right into Clean Architecture is way too much. The pure vertical slice examples I have seen still don't vibe with me *exactly*. My
hybrid approach allows simple features to *stay simple* and for complex features to grow out when necessary.

In the early stages of a project you rarely know the domain, or what you need to build, well enough. So don't write too much code that has
to change or be thrown away. Write good code but don't add unnecessary levels of indirection.

The focus is on the domain, on modeling behavior, and adding complexity/abstracting only when necessary. I used what I've learned from my
other projects and picked what I like.

Some key points of the ones that pop into my head right now:

* Start simple. Grow into complexity when needed.
* Co-location of code. Things that change together, stay together. The higher up in the directory a file is, the more like it's used by
  other parts of the code.
* Similar code != the same code. Some duplication is better than the wrong abstraction.
* Domain focus. Provide value early. Use the language of the domain in the code.
* CQRS. Mutations/writes use domain models. Read operations do queries directly against the DB. Only create separate read tables *when
  necessary*.
* Proper OpenAPI-docs: Be strict about generating great openapi-docs. Allow your frontends to auto-generate types. Saves a lot of time and
  prevents bugs.

## Testing

The Domain models, helper methods, and other suitable classes get unit testing. Otherwise, I lean in favor of integration testing. In the
beginning things move quickly, and unit testing is often too tightly coupled. Integration tests let you refactor properly while still having
some tests.

The Domain project gets unit tests since it doesn't have any dependencies. The API projects get a mix; Unit Tests for smaller things like
extensions, utils, etc. The feature folders get integration tests, using test containers and a real DB. Infrastructure I would really only
test the external parts. Either integration tests against a test environment or mocks for contract testing.

## Hosting, CI/CD, DevOps

It's .NET. It can run anywhere easily. PR's should build and run tests. Merge to main, then run CI/CD, typical trunk-based
development. I prefer having a VPS and use [Coolify](https://coolify.io), but deploying to Azure or other cloud providers is just as easy
(if you have a spending limit).

## Performance

.NET is stupidly fast and can still be written without a lot of code. I start with a monolith, with in-process messaging/events, and only
move towards more complex solutions when we know we actually have to. Fire it up on a VPS (or self-host) and serve thousands of users for
cheap. Scale when you *actually* need to.

## The End

Thanks for reading! This was made quickly so if you spot any errors let me know. Hopefully what I tried to show was clear enough.