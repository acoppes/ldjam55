#!/bin/bash

PROJECT_NAME=$1
export GEMSERK_PROJECT_NAME=${PROJECT_NAME,,}

butler.exe push --ignore "*_DoNotShip" --ignore "*_ButDontShipItWithYourGame" builds/$GEMSERK_PROJECT_NAME/html5 arielsan/$GEMSERK_PROJECT_NAME:html5-latest
butler.exe push --ignore "*_DoNotShip" --ignore "*_ButDontShipItWithYourGame" builds/$GEMSERK_PROJECT_NAME/windows arielsan/$GEMSERK_PROJECT_NAME:windows-latest
# butler.exe push --ignore "*_DoNotShip" --ignore "*_ButDontShip" builds/$GEMSERK_PROJECT_NAME/macos arielsan/$GEMSERK_PROJECT_NAME:mac-latest
# butler.exe push --ignore "*_DoNotShip" --ignore "*_ButDontShip" builds/$GEMSERK_PROJECT_NAME/linux arielsan/$GEMSERK_PROJECT_NAME:linux-latest

