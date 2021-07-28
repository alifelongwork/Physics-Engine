void DrawAxes()
{
  strokeWeight(4);
  line(0, 0, width, 0);
  line(0, 0, 0, height);
}

void DrawOBB()
{
  pushMatrix();
  translate(width/4, height/4);
  rotate(radians(45));
  rect(0, 0, 150, 50);
  strokeWeight(10);
  point(75, 25);
  popMatrix();
}

void setup()
{
  size(400, 400);
  background(255);
  DrawAxes();
  strokeWeight(2);
  DrawOBB();
  save("OBBProj.jpg");
}
