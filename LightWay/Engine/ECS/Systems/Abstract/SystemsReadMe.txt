
A system contains a list of Types, These are the components that your system requires. For example a rendering system may reqire a Possition and a Texture component.

A typical System implementation consists of the following 
	1. A constructor where you add the components that a system need to function. THE CONSTRUCTOR MUST ALWAYS CALL THE Init() FUNCTION (Last) FOR THE SYSTEM TO WORK
	2. A ProcessEntity() Method where you write your system code
		The following is an example of retreaving a component from the workingEntity: ((VelocityC)workingEntity[typeof(VelocityC)])
		This retreves a VelocityC Component and casts is from a IComponent to VelocityC
	3. An optional Update() Override
		The job of the update method is to find an aplicable set of components from a single entity and add that information to the working entity to be processed by ProcessEntity
		Sometimes (such as in the case of the RenderSystem) it may be nessesary to override the default update to allow for more functionallity, In the case of the RenderSystem this 
		is done so that all textures rendered by the system can be all rendered as a single sprite batch 

Notes
	All Systems must inherit from System
	Always remember to call the Init() function in the constructor after you add your components
	For your system to be called you have to add it to an EntityController inside the InitSystems Method