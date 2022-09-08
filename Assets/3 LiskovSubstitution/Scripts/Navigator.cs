
namespace DesignPatterns.LSP
{
    public class Navigator
    {
        // Imagine a turn-based game where you move the Vehicle along a prescribed path: forward, left, right

        // This example violates Liskov Substitution Principle. We cannot substitute the subtype Train
        // for the type Vehicle here.  A train cannot functionally turn left/right:

        //public void Move(Vehicle vehicle)
        //{
        //    vehicle.GoForward();
        //    vehicle.TurnLeft();
        //    vehicle.TurnRight();
        //}

        // Instead, we can use these methods that do follow Liskov Substitution principle.
        // Here, you can use a Car or a Truck wherever we use the type
        // RoadVehicle. You can use a Train where we encounter RailVehicle.

        // Enforce inheritance hierarchy based on software design, not on real-world analogies.

        public void MoveRoadVehicle(RoadVehicle roadVehicle)
        {
            roadVehicle.GoForward();
            roadVehicle.TurnLeft();
            roadVehicle.TurnRight();
        }

        public void MoveRailVehicle(RailVehicle railVehicle)
        {
            railVehicle.GoForward();
            railVehicle.GoForward();
            railVehicle.Reverse();
        }

    }
}
