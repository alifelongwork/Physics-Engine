public class Vector
{
  float X;
  float Y;
  
  public Vector(float x, float y)
  {
     X = x;
     Y = y;
  }
  
  public boolean CompareMaxVector(Vector comp)
  {
    if(this.X >= comp.X && this.Y >= comp.Y)
    {
      return true;
    }
    return false;
  }
  
  public boolean CompareMinVector(Vector comp)
  {
    if(this.X <= comp.X && this.Y <= comp.Y)
    {
      return true;
    }
    return false;
  }
}

public class Shape
{
  Vector Min;
  Vector Max;
  Vector Center;
  float Radius;
  
  Vector Speed = new Vector(1, 1);
  
  public Shape(float xmin, float ymin, float xmax, float ymax)
  {
    if(xmin > xmax)
    {
      float temp = xmax;
      xmax = xmin;
      xmin = temp;
    }
    if(ymin > ymax)
    {
      float temp = ymax; 
      ymax = ymin;
      ymin = temp;
    }
    Min = new Vector(xmin, ymin);
    Max = new Vector(xmax, ymax);
  }
  
  public Shape(float x, float y, float r)
  {
    Center = new Vector(x, y);
    Radius = r;
  }
  
  public boolean RectangleCollide(Shape rect)
  {
    if(this.Max.CompareMaxVector(rect.Min) && this.Min.CompareMinVector(rect.Max))
    {
      return true;
    }
    
    return false;
  }
}

Shape rect1;
Shape rect2;

void setup()
{
  size(500, 500);
  rect1 = new Shape(100, 150, 200, 100);
  rect2 = new Shape(300, 300, 200, 250);
}

void draw()
{
  background(255);
  
  
    rect1.Min.X += rect1.Speed.X;
    rect1.Min.Y += rect1.Speed.Y;
    rect1.Max.X += rect1.Speed.X;
    rect1.Max.Y += rect1.Speed.Y;
    
    rect2.Min.X += rect2.Speed.X;
    rect2.Min.Y += rect2.Speed.Y;
    rect2.Max.X += rect2.Speed.X;
    rect2.Max.Y += rect2.Speed.Y;
    
    if(rect1.Min.X < 0)
    {
      rect1.Speed.X = abs(rect1.Speed.X);
    }
    else if(rect1.Max.X > width)
    {
      rect1.Speed.X = -abs(rect1.Speed.X);
    }
    
    if(rect1.Min.Y < 0)
    {
      rect1.Speed.Y = abs(rect1.Speed.Y);
    }
    else if(rect1.Max.Y > height)
    {
      rect1.Speed.Y = -abs(rect1.Speed.Y);
    }
    
    if(rect2.Min.X < 0)
    {
      rect2.Speed.X = abs(rect2.Speed.X);
    }
    else if(rect2.Max.X > width)
    {
      rect2.Speed.X = -abs(rect2.Speed.X);
    }
    
    if(rect2.Min.Y < 0)
    {
      rect2.Speed.Y = abs(rect2.Speed.Y);
    }
    else if(rect2.Max.Y > height)
    {
      rect2.Speed.Y = -abs(rect2.Speed.Y);
    }
    
    if(rect1.RectangleCollide(rect2))
    {
      fill(0);
    }
    else
    {
      fill(255);
    }
  
  
  
  rect(rect1.Min.X, rect1.Min.Y, abs(rect1.Max.X - rect1.Min.X), abs(rect1.Max.Y - rect1.Min.Y));
  rect(rect2.Min.X, rect2.Min.Y, abs(rect2.Max.X - rect2.Min.X), abs(rect2.Max.Y - rect2.Min.Y));
}
