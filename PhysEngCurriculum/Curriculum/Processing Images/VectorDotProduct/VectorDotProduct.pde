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
  line(width/2, height/2, width/2+ 100,height/2- 50);
//  line(width/2, height/2, width/2+100, height/2-100);
  
  fill(0);
  triangle(width/2+95, height/2-55, width/2+105, height/2-53, width/2+103, height/2-45);
  triangle(width/2+150, height/2+45, width/2+155, height/2+54, width/2+145, height/2+55);
 
  noFill();
  arc(width/2, height/2, 50, 50, -radians(25), radians(20));
  strokeWeight(2);
  fill(0);
  int x = 0;
   for (int y = height/2-50; y <= height/2+20; y+=10)
  {
    // stroke(-(y>>1&4));
    line(width/2+100 - x, y, width/2+98-x, y+6);
    x+=4;
  }
  //for(int x = width/2; x <= width/2+150; x+= 10)
  //{
  //  line(x, height/2-150, x+4, height/2-150);
  //}
}

void setup()
{
  size(400, 400);
  background(255);
  DrawAxes();
  DrawVectors();
}
