public class Vector
{
  float X;
  float Y;
  
  public Vector()
  {
  }
  
  public Vector(float x, float y)
  {
    X = x;
    Y = y;
  }
  
  private Vector Subtraction(Vector v)
  {
    float newX = v.X - this.X;
    float newY = v.Y - this.Y;
    
    return new Vector(newX, newY);
  }
  
  public Vector Distance(Vector v)
  {
    return this.Subtraction(v);
  }
  
  public float Magnitude()
  {
    return sqrt(this.X * this.X + this.Y * this.Y);
  }
}

public class Circle
{
  float Radius;
  Vector Center;
  Vector Speed;
  
  public Circle(float x, float y, float radius)
  {
    Center = new Vector(x, y);
    Radius = radius;
    Speed = new Vector(1, 1);
  }
  
  public boolean CircleCollision(Circle c)
  {   
    Vector dist = this.Center.Distance(c.Center);
    if(dist.Magnitude() < this.Radius/2+c.Radius/2)
    {
      return true;
    }
    return false;
  }
}

public class Rectangle
{
  float X;
  float Y;
  float W;
  float H;
  
  public Rectangle(float x, float y, float w, float h)
  {
    X = x;
    Y = y;
    W = w;
    H = h;
  }
}

Circle c1;
Circle c2;
Rectangle r1;
Rectangle r2;

void setup()
{
  size(500, 500);
  c1 = new Circle(50, 50, 150);
  c2 = new Circle(250, 150, 150);
  r1 = new Rectangle(c1.Center.X-c1.Radius/4, c1.Center.Y-c1.Radius/4, c1.Radius/2, c1.Radius/2);
  r2 = new Rectangle(c2.Center.X-c2.Radius/4, c2.Center.Y-c2.Radius/4, c2.Radius/2, c2.Radius/2);
}

void draw()
{
  background(255);
  
  //if(mousePressed)
  //{
  //  if(mouseButton == LEFT && mouseX < c1.Center.X + c1.Radius/2f && mouseY < c1.Center.Y + c1.Radius/2f && mouseX > c1.Center.X - c1.Radius/2f && mouseY > c1.Center.Y - c1.Radius/2f)
  //  {
  //    c1.Center.X += (mouseX - pmouseX);
  //    c1.Center.Y += (mouseY - pmouseY);
  //    r1.X += (mouseX - pmouseX);
  //    r1.Y += (mouseY - pmouseY);
      
  //  }
  //  if(c1.CircleCollision(c2))
  //  {
  //    fill(0);
  //  }
  //  else
  //  {
  //    fill(255);
  //  }
  //}
  c1.Center.X += c1.Speed.X;
  c1.Center.Y += c1.Speed.Y;
  r1.X = c1.Center.X - c1.Radius/4;
  r1.Y = c1.Center.Y - c1.Radius/4;
  
  c2.Center.X += c2.Speed.X;
  c2.Center.Y += c2.Speed.Y;
  r2.X = c2.Center.X - c2.Radius/4;
  r2.Y = c2.Center.Y - c2.Radius/4;
  
  if(c1.Center.X + c1.Radius/2 > width)
  {
    c1.Speed.X = -abs(c1.Speed.X);
  }
  else if(c1.Center.X - c1.Radius/2 < 0)
  {
    c1.Speed.X = abs(c1.Speed.X);
  }
  
  if(c1.Center.Y + c1.Radius/2 > height)
  {
    c1.Speed.Y = -abs(c1.Speed.Y);
  }
  else if(c1.Center.Y - c1.Radius/2 < 0)
  {
    c1.Speed.Y = abs(c1.Speed.Y);
  }
  
  if(c2.Center.Y + c2.Radius/2 > height)
  {
    c2.Speed.Y = -abs(c2.Speed.Y);
  }
  else if(c2.Center.Y - c2.Radius/2 < 0)
  {
    c2.Speed.Y = abs(c2.Speed.Y);
  }
  
  if(c2.Center.X + c2.Radius/2 > width)
  {
    c2.Speed.X = -abs(c2.Speed.X);
  }
  else if(c2.Center.X - c2.Radius/2 < 0)
  {
    c2.Speed.X = abs(c2.Speed.X);
  }
  
   if(c1.CircleCollision(c2))
    {
      fill(0);
    }
    else
    {
      fill(255);
    }
 
  circle(c1.Center.X, c1.Center.Y, c1.Radius);
  circle(c2.Center.X, c2.Center.Y, c2.Radius);
  fill(255);
  rect(r1.X, r1.Y, r1.W, r1.H);
  rect(r2.X, r2.Y, r2.W, r2.H);
  line(c1.Center.X, c1.Center.Y, c2.Center.X, c2.Center.Y);
}
