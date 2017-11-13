USAGE:
This implementation allows you to call functions in the 3 most used Unity functions: Update, LateUpdate and FixedUpdate functions. This also allows you call run a coroutine function in the main Thread. It can be extended to be able to call functions in other Unity callback functions such as OnPreRender and OnPostRender.

1.First, initialize it from the Awake() function.

	void Awake()
	{
		UnityThread.initUnityThread();
	}
	
2.To execute a code in the main Thread from another Thread:

	UnityThread.executeInUpdate(() =>
	{
		transform.Rotate(new Vector3(0f, 90f, 0f));
	});
	
This will rotate the current Object the scipt is attached to, to 90 deg. You can now use Unity API(transform.Rotate) in another Thread.

3.To call a function in the main Thread from another Thread:

	Action rot = Rotate;
	UnityThread.executeInUpdate(rot);

	void Rotate()
	{
		transform.Rotate(new Vector3(0f, 90f, 0f));
	}
	
The #2 and #3 samples executes in the Update function.

4.To execute a code in the LateUpdate function from another Thread:
Example of this is a camera tracking code.

	UnityThread.executeInLateUpdate(()=>
	{
		//Your code camera moving code
	});
	
5.To execute a code in the FixedUpdate function from another Thread:
Example of this when doing physics stuff such as adding force to Rigidbody.

	UnityThread.executeInFixedUpdate(()=>
	{
		//Your code physics code
	});
	
6.To Start a coroutine function in the main Thread from another Thread:

	UnityThread.executeCoroutine(myCoroutine());

	IEnumerator myCoroutine()
	{
		Debug.Log("Hello");
		yield return new WaitForSeconds(2f);
		Debug.Log("Test");
	}

Finally, if you don't need to execute anything in the LateUpdate and FixedUpdate functions, you should comment both lines of this code below:

//#define ENABLE_LATEUPDATE_FUNCTION_CALLBACK
//#define ENABLE_FIXEDUPDATE_FUNCTION_CALLBACK
This will increase performance.

