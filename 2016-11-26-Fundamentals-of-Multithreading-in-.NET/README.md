<!-- section start -->
<!-- attr: { id:'', class:'slide-title', showInPresentation:true, hasScriptWrapper:true } -->
# Multithreaded programming: Fundamentals
## Brief overview and concepts, Creating and starting threads, Threads synchronization, Locking shared resources, Thread states

<div class="signature">
	<p class="signature-course">Multithreaded programming</p>
	<p class="signature-initiative">Telerik Academy Plus</p>
	<a href="http://academy.telerik.com/seminars/software-engineering/" class="signature-link">http://academy.telerik.com/seminars</a>
</div>




<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Table of Contents
## What will we cover?




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# Table of Contents - I
- [Brief overview and concepts](#overview)
- [Parallel vs Concurrent vs Async](#concurrencyVsParallelism)
- [Process vs. Thread](#processVsThread)
- [Multitasking vs. Multithreading](#multitaskingVsMultithreading)
 - [Cooperative multitasking](#cooperativeMultitasking)
 - [Preemptive multitasking](#preemptiveMultitasking)
- [Multithreading use-cases](#useCases)
- [Multithreading caveats](#caveats)




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# Table of Contents - II
- [Creating and Starting threads](#creatingAndStartingThreads)
- [Parameterized Thread Start](#parameterizedThreadStart)
- [Naming threads](#namingThreads)
- [Background threads](#backgroundThreads)
 - [a.k.a Daemon threads](#daemonThreads)
- [Threads lifetime](#threadLifetime)
- [Thread priority](#threadPriority)
- [Joining a Thread](#joiningThreads)




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# Table of Contents - III
- [Race condition](#raceCondition)
- [Critical sections](#synchronizingThreads)
- [Locking shared resources](#lockingSharedResources)
- [The tricky "volatile" keyword](#volatileKeyword)



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# Table of Contents - IV
- [Thread States](#threadStates)
	- [Unstarted](#unstartedState)
	- [Running](#runningState)
	- [Blocked](#blockedState)
	- [Interrupted](#interruptedState)
	- [StopRequested/Stopped](#stoppedState)
	- [SuspendRequested/Suspended](#suspendedState)
	- [AbortRequested/Aborted](#abortedState)




<!-- section start -->
<!-- attr: { id:'overview', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# <a id="overview"></a> Brief overview
## Understanding asynchronous, parallel and concurrent work




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style: 'font-size:0.8em;' } -->
# Synchronous work
In order to understand what Asynchronous programming is, we must have a clear definition for what **Synchronous programming** is:
- **Sequential** execution of code statements.
- If a blocking operation occurs, the whole process is blocked.
- If a long-running operation occurs, the UI becomes unresponsive.
- CPU-demanding tasks delay the execution of all other tasks.
- Accessing external resources blocks the entire process (reading files from the HDD, querying a web service, etc).




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style: 'font-size:0.8em;' } -->
# Synchronous work<br>Example
- A man is doing presentation for work.
- His wife starts asking random questions, just to get his attention.
- A man can't give attention, because he maxed out his CPU resources on the work he is currently doing, so he is not-responding back to his wife. He is not even listening to what she says.
- After his work is completed, he turns around with a cute smile and asks his wife - "Whatsup? :)", but he has no idea that 30 seconds ago with his silence he accidentally opened Pandora's box.




<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Freezing UI
## Live demo




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style: 'font-size: 0.8em;' } -->
# Asynchronous work
- In general, **asynchronous** (from Greek asyn-, meaning "not with," and chronos, meaning "time") is an adjective describing **objects** or **events** that are **not coordinated in time**.

- In computer programs, asynchronous operation means that a process operates **independently** of other processes.

- A program using asynchronous programming, **dispatches tasks to devices that can take care of themselves**, leaving the program free to do something else until it receives a signal that the work is completed.


<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style: 'font-size:0.9em;' } -->
# Asynchronous work
- **Example:** A woman sits in a cafeteria and orders a coffee. While waiting for her coffee to be served, she is reading the morning newspaper.

<!-- <img showInPresentation="true" class="slide-image" src="images/Async.png" style="top:34%; left:4%; width:65%; z-index:-1; border-radius: 5px; border: 1px solid white;" /> -->
</br>
</br>
</br>
</br>
</br>
</br>
</br>
- Not this one though, she already has a coffee.




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style: 'font-size:0.9em;' } -->
# Asynchronous work
- Example: This guys is asynchronously doing his laundry while reading Twilight.

<!-- <img showInPresentation="true" class="slide-image" src="images/Async2.jpg" style="top:28%; left:4%; width:65%; z-index:-1; border-radius: 5px; border: 1px solid white;" /> -->
</br>
</br>
</br>
</br>
</br>
</br>
</br>




<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Responsive UI
## Live demo




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style: 'font-size:0.9em;' } -->
# Parallel execution
- In parallel computing, a computational task is typically **broken down in several**, often many, very similar **subtasks** that can be **processed independently** and whose results are combined afterwards, upon completion.
- The tasks advance simultaneously - that is **literally at the same time**.




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style: 'font-size:0.9em;' } -->
# Parallel execution
**Examples**:
- A logistics company uses many transport vehicles in order to complete the deliveries as fast as possible.
- An accounting company has many employees that do calculations for multiple clients simultaneously.

Parallel programs distribute their tasks to multiple processors, that actively work on all of them simultaneously.

[Video](https://www.youtube.com/watch?v=3nbjhpcZ9_g)



<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# <a id=""></a> Sum problem
## [Live demo]()




<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# <a id=""></a> Sum of all numbers in a given interval
## [Demo]()




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style: 'font-size:0.9em;' } -->
# Concurrent execution
- **Concurrent** computing is a form of computing in which **several computations** are executed during **overlapping time periods** concurrently instead of sequentially.
- There is a **separate execution point** or "thread of control" **for each computation** ("process").
- A concurrent system is one where a computation can **advance without waiting for all other computations** to complete; where more than one computation can advance at the "same time".




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style: 'font-size:0.9em;' } -->
# Concurrent execution
- **Concurrent** programs handle tasks that are all **progressing at the same time**, but it is only **necessary to work briefly and separately on each task**, so the work can be interleaved in whatever order the tasks require.

- Example: [A man juggling with one hand](https://www.youtube.com/watch?v=9uMui692JHU)




<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Concurrency vs. Parallelism
## Diffrences between concurrency and parallelism




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# Concurrency vs Parallelism
 - Concurrency is about **dealing** with lots of things at once.
 - Parallelism is about **doing** lots of things at once.
 - Not the same concepts, but **related**.
 - One is about structure, the other is about execution.




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# Quote
"In programming, **concurrency** is the composition of **independently executing processes**,  
while **parallelism** is the **simultaneous execution** of (possibly related) computations."  
\- Andrew Gerrand -  

<br/>
[Rob Pike: "Concurrency is not Parallelism" (Video)](https://vimeo.com/49718712)





<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Process vs. Thread




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# What is a Process?
 - In terms of computing - a process is an **instance** of a **computer program** that is being **executed**.  
 - It contains the **program code** and its **current activity (state)**.   
 - Depending on the **OS**, a process may be made up of **multiple threads of execution**, that execute instructions <a href="https://en.wikipedia.org/wiki/Concurrency_(computer_science)">_concurrently_</a>.




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# What is a Process?
 - A computer program is a _**passive collection**_ of instructions, while a process is the _**actual execution**_ of those instructions.   
 - _**Several processes**_ may be associated with the _**same program**_. For example, opening up several instances of the same program (Visual Studio, Google Chrome, etc), often means more than one process is being executed.




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# What is a Process?
 - Each process **provides the resources** needed to execute a program.
 - Each process is started with a **single thread of execution**, often called the **primary thread**, and can create additional threads from any of its threads.




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# What is a Process?
 - A process has a:
  - **Virtual address space**
  - **Executable code**
  - **Open handles to system objects** & **security context**
  - **Unique process identifier**
  - **Environment variables** & **priority class**
  - **At least one thread of execution**.




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# What is a Thread?
 - A thread is an **entity within a process** that can be **scheduled** for execution.
 - All threads of a **process** share its **virtual address space** and system resources.
 - Each thread maintains **exception handlers**, a **scheduling priority**, thread **local storage**, a unique **thread identifier**, and a **set of structures** the system will use to save the **thread context** until it is **scheduled**.



 <!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# What is a Thread?
 - The **thread context** includes:
	 - **The thread's set of machine registers**
	 - **The kernel stack**
	 - **A thread environment block (TEB)**
	 - **A user stack in the address space of the thread's process**.
 - Threads can also have their own **security context**, which can be used for impersonating clients.




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style: 'font-size:0.9em;' } -->
# Process anatomy<br> Summary
 - A process is an inert container
  - It defines a virtual address space.
	 - Contents are not addressable from another processes.
  - Libraries of code are mapped into the address space.
	 - 1 EXE + N DLLs (dynamically loaded libraries)

<!-- <img showInPresentation="true" class="slide-image" src="images/ProcessAnatomy1.png" style="top:57%; left:8%; width:80%; z-index:-1; border-radius: 5px; border: 1px solid white;" /> -->




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style: 'font-size:0.9em;' } -->
# Thread anatomy<br> Summary
- `Threads` execute code
 - A thread is a path of execution within a single process.
 - It has access to all of the data within that process.
 - Each thread has its own callstack & copy of the CPU registers
 - A process with no threads exits (because it can no longer perform work)

<!-- <img showInPresentation="true" class="slide-image" src="images/ThreadAnatomy1.png" style="top:62%; left:40%; width:27%; z-index:-1; border-radius: 5px; border: 1px solid white;" /> -->
<!-- section start -->




<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Multithreading vs. Multitasking




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# What is Multitasking?
 - <a href="https://en.wikipedia.org/wiki/Computer_multitasking">_**Multitasking**_</a> is a concept of **performing multiple tasks** (processes) over a certain **period of time** by executing them in a **parallel** manner or **concurrently**.  
 - **New tasks start** and **interrupt already started ones** before they have reached completion, **instead of executing the tasks sequentially** so each started task needs to reach its end before a new one is started.  




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# What is Multitasking?
 - The tasks share common processing resources such as central processing units **(CPUs)** and **main memory**.

 - Multitasking does not necessarily mean that multiple tasks are executing at exactly the same time. **Multitasking does not imply parallel execution**, but it does mean that more than one task can be **part-way** through execution at the same time, and that **more than one task** is **advancing** over a given period of time.




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# What is Multithreading?
- Multithreading **extends the idea of multitasking** into applications, so you can **subdivide specific operations** within a **single application** into individual threads. Each of the threads can run in **parallel** or/and **concurrently**. The OS **divides processing time** not only among **different applications**, but also among **each thread** within an application.




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# What is Multithreading?
- A common example of the **advantage of multithreading** is the fact that you can have a **word processor** that **prints a document** using a background thread, but at the same time another thread is running that **accepts user input**, so that you can type up a new document.




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# Summary
- **Multitasking** is the **ability of an OS** to run **several tasks**(processes) at the "**same time**".  

- Switching between the tasks is so fast that the user can interact fully with the system, **without having to wait for one task to be completely finished** (at least he does not feel like waiting, in practice - he is waiting a couple milliseconds).




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Summary
- **Multithreading** is the **ability of an OS** to execute **different parts of a program**, called threads, **in parallel** and/or **concurrently**.  

- Multithreading usually involves very sophisticated programs that use multiple CPUs at the same time to **improve performance and responsiveness**.   

- A computer with multiple CPUs which does not have applications written specifically to use multiprocessing or multithreading uses just one CPU.




<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Preemptive multitasking




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Preemptive multitasking
- In computing, preemption is the act of **temporarily interrupting** a task being carried out by a computer system, **without requiring its cooperation**, and with the intention of **resuming the task** at a later time.
- Such changes of the executed task are known as _**Context switching**_.
- It is normally carried out by a privileged task or part of the system known as a preemptive scheduler, which has the power to preempt, or interrupt, and later resume, other tasks in the system.




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Context switching
- In computing, a **context switch** is the process of **storing** and **restoring the state** (the execution context) of a **process** or a **thread** so that execution can be resumed from the same point at a later time.  

- This enables multiple processes to share a **single CPU** and is an essential feature of a **multitasking** operating system.




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Context switching cost
- Context switches are usually **computationally intensive**, and much of the design of OS is to optimize the use of context switches.
- Switching from one process to another requires a certain amount of **time** for doing the administration - **saving** and **loading** registers and memory maps, updating various tables and lists, etc.
- Switching between threads of a single process can be faster than between two separate processes, because threads share the **same virtual memory maps**, so a [TLB](https://en.wikipedia.org/wiki/Translation_lookaside_buffer) flush is not necessary.




<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Cooperative multitasking
## (non-preemptive multitasking)




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Cooperative multitasking
- Cooperative multitasking is a style of computer multitasking in which the **OS never initiates a context switch** from a running process to another process.  

- Instead, processes voluntarily [yield control](https://en.wikipedia.org/wiki/Yield_(multithreading) periodically or when idle in order to enable multiple applications to be run simultaneously.  

- All programs must cooperate for the entire scheduling scheme to work.




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Cooperative multitasking
- As a cooperatively multitasked system relies on _**each process regularly giving up time to other processes**_ on the system, one poorly designed program can consume all of the CPU time for itself, either by performing **extensive calculations** or by **busy waiting**.

- Both would cause the whole system to **hang**. In a server environment, this is a **hazard** that makes the entire environment unacceptably **fragile**.


<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# Cooperative multitasking
- However, cooperative multitasking allows much **simpler implementation** of applications because their execution is **never unexpectedly interrupted** by the process scheduler.



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Multithreading use-cases



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.8em;' } -->
# Multithreading use-cases
<br>
The benefits of leveraging multi-threading include:
- Opportunity to **scale** by parallelizing CPU-bound operations.
 - assuming that a multi-core/multi-processor hardware is used.  
- **Perform** CPU-bound **work while** I/O operations are **waiting**.  
- **Maintain a responsive user interface**.
 - farming off lengthy and/or blocking operations to a separate thread.
 - using thread priorities to ensure the UI thread has highest priority.




<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Multithreading caveats




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.8em;' } -->
# Multithreading caveats
The price to pay for multi-threading includes:
- Slower execution time on single-core/processor machines.
 - context-switching overhead

<!-- <img class="slide-image" showInPresentation="true" src="images\Caveats.PNG" style="top:35%; left:1%; width:100%; z-index:-1; border: 1px solid white; border-radius:5px;" /> -->
<br><br><br>
- Added program complexity
 - More lines of code
 - Less readable/maintainable
 - Difficult to debug
 - Difficult to test




<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Creating and starting threads
## In a .NET managed environment



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.8em;' } -->
# Creating threads
- In .NET you can explicitly create a new thread using the `Thread` class from the `System.Threading` namespace.

```cs
public class Program
{
    static void Main(string[] args)
    {
        var thread = new Thread(DoWork);
        thread.Start();

        // Some code to be executed
        // on the MAIN thread
    }

    static void DoWork()
    {
        // Some code to be executed
        // on the NEW thread
    }
}
```



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.8em;' } -->
# Creating threads
<br>
- The `Thread` constructor accepts only methods that match the signature of the internal delegates `ThreadStart` or `ParameterizedThreadStart`:

```cs
public delegate void ThreadStart();

public delegate void ParameterizedThreadStart(object obj);
```
- A newly created `Thread` is **scheduled for starting** using the instance method **Thread.Start()**



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Creating threads
## Live demo



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Parameterized thread start




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.8em;' } -->
# Parameterized thread start
- A parameterized thread start allows us to pass data to the thread that will be executed.

```cs
static void Main(string[] args)
{
	 var thread = new Thread(DoWork);
	 var threadStartOptions = new ThreadStartOptions
	 {
			 ForegroundColor = ConsoleColor.Magenta,
			 Message = "Parameters, parameters, parameters..."
	 };
	 thread.Start(threadStartOptions);
}

static void DoWork(object threadStartOptions)
{
	 var opts = threadStartOptions as ThreadStartOptions;
	 Console.ForegroundColor = opts.ForegroundColor;
	 Console.WriteLine(opts.Message);
}
```



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Parameterized thread start
## Live demo



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Naming threads



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.8em;' } -->
# Naming threads
- In .NET managed environment, you can set a name for your thread in order to make debugging and testing more comprehensive.

```cs
public static void Main(string[] args)
{
		var cukiThread = new Thread(SayWhatsup);
		var cukiThreadStartOptions = new ThreadStartOptions { Color = ConsoleColor.Magenta, SleepTime = 1000 };
		cukiThread.Name = "Cuki";
		cukiThread.Start(cukiThreadStartOptions);
}

public static void SayWhatsup(object threadStartOptions)
{
	 var opts = threadStartOptions as ThreadStartOptions;
	 var currentThreadName = Thread.CurrentThread.Name;
	 Console.ForegroundColor = opts.Color;
	 Console.WriteLine($"Thread [{currentThreadName}] said: Hello");
}
```


<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Naming threads
## Demo



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Background threads
## (known as Daemon threads in other programming languages)



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Background threads
- In .NET, a managed thread is either a **background thread** or a **foreground thread**.  

- Background threads are identical to foreground threads with one exception: a **background thread does not keep the managed execution environment running**.  

- Once all **foreground** threads have **finished** execution, the CLR **stops all background** threads and shuts down the process.



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.8em;' } -->
# Background threads
- We can use the **Thread.IsBackground** property to determine whether a thread is background or a foreground thread, or to change its status. A thread can be changed to a background thread at any time by setting its **IsBackground** property to **true**.  

- **IMPORTANT!** - The foreground or background status of a thread does not affect the outcome of an unhandled exception in the thread. An **unhandled exception in any thread results in termination of the process**.



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Background threads
## Live demo



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Threads lifetime
## Managed thread objects and OS threads




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Threads lifetime
- Consider the behavior of the following code:

```cs
if (true)
{
	 new Thread(() =>
	 {
		 ExecuteLongRunningTask();
	 }).Start();
}

while (true)
{
	 //do nothing
	 Thread.Sleep(100);
}
```

- Will the thread be suspended after the GC collects the "Thread" object that is no longer referenced in the execution scope?



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true } -->
# Threads lifetime
- `Thread` objects will be eligible for garbage collection as soon as they are no more used.
- In our example - after calling the `Start()` method, the `Thread` object will be immediately scheduled for GC. It will however not be collected immediately, as the GC runs at specific times. [Learn more about GC .](http://www.red-gate.com/products/dotnet-development/ants-memory-profiler/learning-memory-management/memory-management-fundamentals)
- However, the actual **OS thread** is not relying on the .NET `Thread` object and will continue to run even if the `Thread` object is collected.




<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Threads lifetime
## Demo




<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Threads priority



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Threads priority
- In .NET we can get or set a Thread's priority level using the **Thread.Priority** property.
- A thread can be assigned any one of the following priority levels:
 - **Highest** / **AboveNormal** / **Normal** / **BelowNormal** / **Lowest**  
- A thread priority brings meta-information for the OS, and shows which thread must receive more CPU time compared to other threads.  
- However, operating systems are not required to honor the priority of a thread.




<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Threads priority
## Live demo



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Joining threads
## (waiting for a thread to finish execution)




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.8em;' } -->
# Joining threads
- The `Thread.Join()` method, blocks the calling thread until the thread represented by this instance terminates.

```cs
public static void Main(string[] args)
{
	 var myThread = new Thread(DoWork);
	 myThread.Start();

	 // Do some work on the main thread...

	 // The main thread waits here
	 // untill 'myThread' finishes execution
	 myThread.Join();

	 // Do the rest of the work on the main thread
}

public static void DoWork(){ ... }
```



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Joining threads
## Live demo



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Race condition
## (common problems when accessing shared resources)



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.8em;' } -->
# Race condition
- In computer programming, a race condition occurs when two or more threads access shared data and they try to change it at the same time.  

- Because the scheduler can swap between threads at any time, or the threads might run in parallel, you don't know the order in which the threads will attempt to access the shared data.  

- Problems often occur when one thread does a "check-then-act", and another thread changes the value in between the "check" and the "act" phases.



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.8em;' } -->
# Race condition
- In computer memory or storage, a race condition may occur if commands to **read** and **write** a large amount of data are received **at almost the same instant**, and the machine attempts to overwrite some or all of the old data while that old data is still being read.

- The result may be one or more of the following:
 - Computer crash
 - "Illegal operation" notification and shutdown of the program
 - Errors when reading the old data
 - Errors when writing the new data



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Race condition
## Demo




<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Critical sections



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.8em;' } -->
# Critical sections
- In computer programming, concurrent or parallel access to **shared resources** (**data structures**, **peripheral devices** or a **network connection**) can lead to unexpected or erroneous behavior.  

- Parts of the program where the <br>shared resource is accessed by <br>multiple threads is called a<br> **critical section** or **critical region**.

<!-- <img class="slide-image" showInPresentation="true" src="images\CriticalSections.jpg" style="top:38%; left:60%; width:38%; z-index:-1; border: 1px solid white; border-radius:5px;" /> -->


<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Critical sections
- Running more than one thread inside the same application does not by itself cause problems.

- The problems arise when multiple threads access the same resources. For instance the same memory (variables, arrays or objects), systems (databases, web services) or files.



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Critical sections
- Here is a critical section code example that might fail if executed by multiple threads simultaneously:

```cs
public class Counter
{
	private long count = 0;

	public void Add(long valueToAdd)
	{
		this.count = this.count + valueToAdd;
	}
}
```

- Imagine if two threads, **A** and **B**, are executing the Add() method on the same instance of the `Counter` class. What will happen?




<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Critical sections
- There is no way to know when the operating system switches between the two threads.  

- The code in the Add() method is not executed as a single atomic instruction. Rather it is executed as a set of individual instructions, similar to this:
 - 1. Read **this.count** from the memory into the register
 - 2. Add **value** to the register
 - 3. Write the register into the memory

- This is a massive prerequisite for potential problems to occur.



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Critical sections
- Consider the following mixed execution of the previous code of threads **A** and **B**

```cs
this.count = 0;

A:  Reads this.count into a register (0)
B:  Reads this.count into a register (0)
B:  Adds value 2 to a register
B:  Writes register value (2) back to memory.
        this.count now equals 2
A:  Adds value 3 to a register
A:  Writes register value (3) back to memory.
        this.count now equals 3
```


<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Critical sections
- The two threads wanted to add the values **(2)** and **(3)** to the **counter**. Thus the value should have been **(5)** after the two threads complete execution.

- However, since the execution of the two threads is interleaved, the result ends up being different.

- The code in the **Add()** method contains a **critical section**. When **multiple threads** execute this critical section, a **race condition** occurs.



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Critical sections
- To prevent race conditions from occurring you must make sure that the critical section is executed as an **atomic instruction**.
- That means that once a **single thread is executing it**, **no other threads should be able to execute it**, until the first thread has left the critical section
- Race conditions can be avoided with proper **thread synchronization** in the critical sections.
- Simple thread synchronization can be achieved using the **lock()** statement.



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Critical sections
## Demo



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Locking shared resources
## using the lock() statement



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.9em;' } -->
# Locking shared resources
- The **`lock`** keyword marks a statement block as a **critical section** by obtaining the mutual-exclusion lock for a given object, **executes** the statement block, and then **releases** the `lock`.
- The `lock` keyword ensures that one thread does not enter a critical section of code while another thread is in the critical section.
- If **another thread** tries to enter a **locked** code block, it will **wait/block**, until the lock object is released.



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.8em;' } -->
# Locking shared resources
- Best practice is to define a **private object** to lock on, or a **private static object** variable to protect data common to all instances.
- You can't use the _**await**_ keyword in the body of a **lock** statement.

```cs
public class Counter
{
	private object myLock = new object();
	private long count = 0;

	public void Add(long valueToAdd)
	{
		lock(this.myLock) // this code block is now
		{                 // executed as an atomic operation
			this.count = this.count + valueToAdd;
			// cannot use 'await' here
		}
	}
}
```



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Locking shared resources
## Demo



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# The "volatile" keyword



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.8em;' } -->
# Volatile
- The **volatile** keyword indicates that a field might be modified by multiple threads that are executing at the same time.
- Fields that are declared volatile are **not subject to compiler optimizations** that assume access by a single thread.
- This ensures that the most up-to-date value is present in the field at all times.
- The volatile modifier is usually used for a field that is accessed by multiple threads.



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# The "volatile" keyword
## Live demo




<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Thread states
## in the .NET managed environment



<!-- attr: { id:'', showInPresentation:true, hasScriptWrapper:true, style:'font-size:0.8em;' } -->
# Thread states
- The ThreadState enumeration is of interest only in a few **debugging scenarios**. Your code should **never use the thread state to synchronize** the activities of threads.  

- **ThreadState** defines a set of all possible execution states for threads. Once a thread is created, it is in **at least one of the states** until it terminates. Threads created within the CLR are initially in the **Unstarted** state.  

- Not all combinations of ThreadState values are valid; for example, a thread cannot be in both the Aborted and Unstarted states.




<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Thread states
## Live demo



<!-- section start -->
<!-- attr: { id:'', class:'slide-section', showInPresentation:true, hasScriptWrapper:true } -->
# Questions?




<!-- attr: { showInPresentation:true, hasScriptWrapper:true } -->
# Free Trainings @ Telerik Academy
- C# Programming @ Telerik Academy
  - Telerik Software Academy
    - academy.telerik.com
  - Telerik Academy @ Facebook
    - facebook.com/TelerikAcademy
  - Telerik Software Academy Forums
    - telerikacademy.com/Forum/Home  
