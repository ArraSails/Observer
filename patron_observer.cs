using System;
using System.Collections;

interface IObserver
{
    void Update(SubjectEvent subjectEvent);
}

interface ISubject
{
   void Subscribe(IObserver observer);
   void Unsubscribe(IObserver observer);
   void NotifyObservers(SubjectEvent subjectEvent);
}

class SubjectEvent
 {
    public string EventType { get; set; }
    public DateTime EventDate { get; set; }
 }
 
 class ConsoleObserver : IObserver
{
    public void Update(SubjectEvent subjectEvent)
    {
       Console.WriteLine("An event just happened!");
       Console.WriteLine("Event type: " + subjectEvent.EventType);
       Console.WriteLine("Date: " + subjectEvent.EventDate);
    }
}

class ProductService : ISubject
{
    private readonly IList observers;
    public ProductService() {
       observers = new List();
    }
    public void Subscribe(IObserver observer) {
        observers.Add(observer);
    }
 
    public void Unsubscribe(IObserver observer) {
        observers.Remove(observer);
    } 
    
    public void AddProduct(string productName) {
        //Business logic to validate and add a product.
        var subjectEvent = new SubjectEvent { 
            EventType = "ProductAdded",
            EventDate = DateTime.Now
        };
        NotifyObservers(subjectEvent);
    }
    public void NotifyObservers(SubjectEvent subjectEvent) {
        Console.WriteLine("Before notifying observers");
        foreach(IObserver observer in observers) {
            observer.Update(subjectEvent);
        }
        Console.WriteLine("After notifying observers");
     }
 }
 
 class Program {
      static void Main(string[] args)
      {
            IObserver observer = new ConsoleObserver();
            ProductService subject = new ProductService();
            subject.Subscribe(observer);
            subject.AddProduct("Solid-state drive");
            Console.WriteLine();
            subject.Unsubscribe(observer);
            subject.AddProduct("Bluetooth mouse");
      }
}
