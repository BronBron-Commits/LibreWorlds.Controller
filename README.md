LibreWorlds.Controller
Overview

LibreWorlds.Controller is a native Windows controller application used to
observe, drive, and validate the real LibreWorlds client pipeline.

It acts as an orchestration and observability layer over the LibreWorlds stack,
wiring together the SDK, World Adapter, and runtime engine while exposing their
true lifecycle state through a live UI.

If a subsystem indicator is active, that part of the stack is actually
connected and running. There are no simulated states or UI-driven shortcuts.

This project exists to prove real end-to-end control, not to render worlds or
act as a game client.

What This Is

A lifecycle controller for LibreWorlds clients

A live visualization of real adapter and SDK state

A coordination surface for bringing subsystems online

A diagnostic tool for validating integration boundaries

An “air traffic controller” for complex client startup flows

What This Is NOT

Not a renderer

Not a game client

Not a mock or demo UI

Not a fake state machine

Not responsible for networking, rendering, or world logic

The Controller never invents state.
It only reflects what the system has actually reached.

Architecture (High Level)

UI (WPF)
-> ViewModel
-> WorldAdapter
<- LibreWorldsBridge
<- LibreWorlds SDK
<- Network / Protocol

The direction of authority is strictly one-way.
State always flows upward from the SDK into the adapter and then into the UI.

Current Capabilities

Live visualization of WorldAdapter lifecycle state

Real adapter-driven state transitions

Clean separation between UI, adapter, and SDK

Verified end-to-end signal propagation without fake transitions

Intended Use

This project is used during development to:

Prove connection, authentication, and world-entry flow

Observe adapter behavior during real SDK events

Debug lifecycle issues without rendering overhead

Serve as a launch and coordination surface for future clients
(Godot, native, mobile)
