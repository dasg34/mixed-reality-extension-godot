using Godot;
using System;

public class RigidBody2 : RigidBody
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    Area area;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        area = GetNode<Area>("Area");

        //ApplyCentralImpulse(new Vector3(10, 0, 0));
        //AddTorque(new Vector3(100, 0, 0));
        Connect("body_entered", this, nameof(_on_RigidBody_body_entered));
        Connect("body_shape_entered", this, nameof(OnBodyExit));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
    private void OnBodyExit(int bodyId, Node body, int bodyShape, int areaShape)
    {
        if (body == this)
        {
            GD.Print("Skip!!!!!!!!");
            return;
        }
        var state = PhysicsServer.BodyGetDirectState(GetRid());
        GD.Print("GetContactCount" + state.GetContactCount());
        GD.Print("GetContactLocalNormal" + state.GetContactLocalNormal(0));
        GD.Print("GetContactLocalNormal.IsNormalized" + state.GetContactLocalNormal(0).IsNormalized());
        GD.Print("GetContactImpulse" + state.GetContactImpulse(0));
        GD.Print("GetContactLocalPosition" + state.GetContactLocalPosition(0));
        GD.Print("GetContactColliderVelocityAtPosition" + state.GetContactColliderVelocityAtPosition(0));
        GD.Print("GetContactColliderVelocityAtPosition.Length()" + state.GetContactColliderVelocityAtPosition(0).Length());
        GD.Print("GetContactColliderPosition" + state.GetContactColliderPosition(0));
        GD.Print("LinearVelocity" + state.LinearVelocity);
        GD.Print("LinearVelocity.Length" + state.LinearVelocity.Length());
        GD.Print("CenterOfMass" + state.CenterOfMass);
        GD.Print("CenterOfMass.length" + state.CenterOfMass.Length());
        GD.Print("InverseInertia" + state.InverseInertia);
        GD.Print("Step" + state.Step);
        Mass = 3;
        //ApplyCentralImpulse(new Vector3(1000, 0, 0));

        //GD.Print("_on_Area_body_entered"+ body);
    }
    private void _on_RigidBody_body_entered(Node body)
    {
        //GD.Print(area.GetOverlappingBodies().Count);
        //GD.Print("on_body_entered"+ body);
    }
}
