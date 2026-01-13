# GestureCommandEngine

## Overview

**GestureCommandEngine** is a lightweight .NET library that adds mouse gesture support to desktop applications.

The library allows applications to recognize mouse gestures based on pointer movement, map multiple gestures to high-level commands, and react to those commands via events — without coupling gesture recognition logic to UI frameworks or application-specific behavior.

### Key concepts

* **Mouse gesture recognition**
  Raw pointer movement (`Point2D` sequences) is analyzed and converted into normalized directional gestures (Up, Down, Left, Right).

* **Command-based architecture**
  Gestures are mapped to semantic `CommandId`s representing user intent.
  Multiple gestures can trigger the same command, similar to how keyboard shortcuts work in modern IDEs.

* **Event-driven execution**
  The engine does not execute application logic directly.
  Instead, it raises events when a command is invoked, allowing the host application to decide how to handle it.

* **Clean separation of concerns**

  * Gesture recognition
  * Gesture-to-command mapping
  * Command invocation
    are implemented as independent components.

### Design goals

* UI-framework agnostic core (no WPF / WinForms dependencies)
* Clear boundaries between infrastructure and application logic
* Testable and extensible architecture
* Simple integration into existing .NET applications

### Typical usage

* Capture mouse movement in the UI layer
* Pass collected points to the `GestureCommandHandler`
* Subscribe to command invocation events
* Execute application-specific actions in response

GestureCommandEngine is intended as a reusable building block for adding gesture-based interactions to .NET desktop applications.
