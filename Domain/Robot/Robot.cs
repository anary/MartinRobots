using Domain.Abstractions;
using Domain.Commons;
using Domain.Events;
using Domain.Services;

namespace Domain.Robot;
public class Robot : AggregateRoot
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Orientation Orientation { get; private set; }
    public bool IsLost { get; private set; }

    public static Robot Land(Guid id, string x, string y, Orientation orientation)
    {
        var robot = new Robot();
        robot.ApplyChange(new RobotLandedEvent(id, x, y, orientation));
        return robot;
    }

    public void TurnLeft()
    {
        if (IsLost) return;
        ApplyChange(new RobotTurnedLeftEvent(Id, (Orientation)(((int)Orientation + 3) % 4)));
    }

    public void TurnRight()
    {
        if (IsLost) return;
        ApplyChange(new RobotTurnedRightEvent(Id, (Orientation)(((int)Orientation + 1) % 4)));
    }

    public void MoveForward(MarsGridService grid)
    {
        if (IsLost) return;
        var (dX, dY) = Orientation.ToVector();
        int nextX = X + dX;
        int nextY = Y + dY;
        if (grid.IsOutofBounds(nextX, nextY))
        {
            if (grid.IsScented(X, Y))
            {
                return;
            }
            ApplyChange(new RobotLostEvent(Id));
            grid.AddScent(X, Y);
        }
        else
        {
            ApplyChange(new RobotMovedForwardEvent(Id, nextX, nextY));
        }
    }

    protected override void When(DomainEvent @event)
        => When((dynamic)@event);

    private void When(RobotLandedEvent e)
    {
        Id = e.AggregateId;
        X = int.Parse(e.X);
        Y = int.Parse(e.Y);
        Orientation = e.Orientation;
    }

    private void When(RobotTurnedLeftEvent e)
        => Orientation = e.NewOrientation;

    private void When(RobotTurnedRightEvent e)
        => Orientation = e.NewOrientation;

    private void When(RobotLostEvent e)
    {
        IsLost = true;
        // you could log or handle the lost event here
    }

    public override string ToString()
    {
        string status = $"{X} {Y} {Orientation}";
        return IsLost ? status + " LOST" : status;
    }
}
