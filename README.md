# SCSE23-0460---Use-of-Large-Language-Models-for-Personalized-and-Accessible-Mental-Health-Support

Welcome to our Unity project repository! This project contains two main scenes: `WebGLCustomization` and `Dialogue`. Below, you'll find instructions on how to set up and run these scenes. The Dialogue scene requires an OpenAI API key to function properly.

## Prerequisites
Before you begin, ensure you have the following installed:

[Git](https://git-scm.com/downloads)
[Unity](https://unity.com/download) (version 2020.3 LTS or later recommended)
An OpenAI API key, which you can obtain from OpenAI

## Cloning the Repository
To clone the repository and start working with the project, follow these steps:

```
git clone https://github.com/Aortz/SCSE23-0460---Use-of-Large-Language-Models-for-Personalized-and-Accessible-Mental-Health-Support.git
cd SCSE23-0460---Use-of-Large-Language-Models-for-Personalized-and-Accessible-Mental-Health-Support
```

## Setup
1. Open the Project in Unity:

* Open Unity Hub.
* Click on 'Add' and navigate to the directory where you cloned the project.
* Select the project folder to add it to the Unity Hub and click on it to open.

2. Configuring the API Key for the Dialogue Scene:

* Navigate to the ChatGPTManager.cs class using Assets > Scripts > Chat Interface from the root directory.
* Find the variable 'apiKey' and paste your OpenAI API key into this file. Save and close the file.

## Running the Scenes
### WebGLCustomization Scene
To run the WebGLCustomization scene:

* Navigate to the Scenes folder in the Unity Project.
* Open the WebGLCustomization.unity scene.
* Press the 'Play' button in Unity to run the scene.

### Dialogue Scene
To run the Dialogue scene:

* Ensure your API key is set up as described above.
* WebGLCustomization shoulkd automatically direct you to the Dialogue Scene
* Else, open the Dialogue.unity scene from the Scenes folder.
* Press the 'Play' button in Unity to start interacting with the dialogue system.

## Building the Project
If you wish to build the project:

* Go to File > Build Settings in Unity.
* Select the platform you want to build for.
* Click Build and select an output directory.

## Support
For support, please open an issue on the GitHub repository or contact the project maintainers.
