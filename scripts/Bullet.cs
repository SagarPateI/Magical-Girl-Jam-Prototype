using Godot;
using System;

public partial class Bullet : Area2D
{
    [Export] public float Speed = 300f;
    [Export] public float Acceleration = 0f;
    [Export] public float Direction = 0f; // Radians
    [Export] public float AngularVelocity = 0f;
    [Export] public Curve SpeedCurve;
    
    private float _currentSpeed;
    private Vector2 _velocity;

    public override void _Ready()
    {
        _currentSpeed = Speed;
    }

    public override void _PhysicsProcess(double delta)
    {
        // Apply acceleration
        if (Acceleration != 0)
            _currentSpeed += Acceleration * (float)delta;
            
        // Apply speed curve
        if (SpeedCurve != null)
            _currentSpeed = Speed * SpeedCurve.Sample((float)GetProcessDeltaTime());
        
        // Apply rotation
        if (AngularVelocity != 0)
            Rotation += AngularVelocity * (float)delta;
        
        // Movement
        _velocity = new Vector2(Mathf.Cos(Direction), Mathf.Sin(Direction)) * _currentSpeed * (float)delta;
        Position += _velocity;
        
        // Screen boundary check
        if (Position.Y < -50 || Position.Y > GetViewportRect().Size.Y + 50)
            QueueFree();
    }
}