using Godot;
using System;

public class Spatial2222 : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var rb = new RigidBody();
        AddChild(rb);

        var cs = new CollisionShape() { Shape = new SphereShape() };
        rb.AddChild(cs);

        rb.AddChild(new MeshInstance() {Mesh = new SphereMesh()});

        rb.Mass = 3;
        rb.AddCentralForce(new Vector3(1000, 0, 0));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
