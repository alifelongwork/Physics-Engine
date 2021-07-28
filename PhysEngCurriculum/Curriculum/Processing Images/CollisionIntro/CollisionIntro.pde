class Shape
{
  int X;
  int Y;
  int W;
  int H;
  int XSpeed;
  int YSpeed;

  public Shape(int x, int y, int w, int h, int xSpeed, int ySpeed)
  {
    X = x;
    Y = y;
    W = w;
    H = h;
    XSpeed = xSpeed;
    YSpeed = ySpeed;
  }
}

Shape rect1;
Shape rect2;
Shape rect3;
Shape rect4;

void setup()
{
  size(500, 500);
  rect2 = new Shape(width - 100, height/2 - 200, 100, 100, 1,1);
  rect1 = new Shape(0, height/2 - 200, 100, 100, 1, 1);
  rect3 = new Shape(0, height/2 + 100, 100, 100, 1, 1);
  rect4 = new Shape(width - 50, height/2+100, 100, 100, 1, 1);
}

void draw()
{
  clear();
  background(255);
  
  rect1.X += rect1.XSpeed;
  rect2.X -= rect2.XSpeed;
  rect3.X += rect3.XSpeed;
  rect4.X -= rect4.XSpeed;
  
  if(rect1.X + rect1.W > rect2.X)
  {
    rect1.XSpeed = 0;
    rect2.XSpeed = 0;
    fill(0);
  }
  
  rect(rect1.X, rect1.Y, rect1.W, rect1.H);
  rect(rect2.X, rect2.Y, rect2.W, rect2.H);
  fill(255);
  rect(rect3.X, rect3.Y, rect3.W, rect3.H);
  rect(rect4.X, rect4.Y, rect4.W, rect4.H);
}
