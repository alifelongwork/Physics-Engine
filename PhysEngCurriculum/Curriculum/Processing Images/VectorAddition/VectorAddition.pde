void DrawAxes()
{
  strokeWeight(5);
  line(0, 0, 0, height);
  line(0, 0, width, 0);
}

void DrawVectors()
{
  strokeWeight(2);
  line(width/2, height/2, width/2 + 150,height/2+ 50);
  line(width/2+150, height/2+50, width/2+ 100,height/2- 100);
  line(width/2, height/2, width/2+100, height/2-100);
  
  fill(0);
  triangle(width/2+95, height/2-100, width/2+102, height/2-105, width/2+102, height/2-98);
  triangle(width/2+150, height/2+45, width/2+155, height/2+54, width/2+145, height/2+55);
 
  
}
void setup()
{
  size(400, 400);
  background(255);
  DrawAxes();
  DrawVectors();
}
