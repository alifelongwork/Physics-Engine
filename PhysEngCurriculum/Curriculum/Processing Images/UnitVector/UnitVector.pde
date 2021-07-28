void DrawAxes()
{
  strokeWeight(2);
  line(0, height/2, width, height/2);
  line(width/2, 0, width/2, height);
}
void setup()
{
  size(400, 400);
  background(255);
  DrawAxes();
  strokeWeight(5);
  line(width/2, height/2, width / 2+50, height / 2);
  line(width/2, height/2, width/2, height/2 - 50);
  strokeWeight(4);
  line(width/2, height/2, width/2 + 150, height/2);
  line(width/2, height/2, width/2, height/2 - 150);
  line(width/2, height/2, width/2 + 150, height/2 - 150);
  
  fill(0);
  triangle(width / 2+50, height / 2 -5, width / 2  +55, height / 2, width / 2 + 50, height / 2+5);
  triangle(width/2-5, height/2-50, width/2, height/2-55, width/2+5, height/2-50);
  triangle(width/2-5, height/2-150, width/2, height/2 - 155, width/2+5, height/2-150);
  triangle(width/2+150, height/2-5, width/2+155, height/2, width/2+150, height/2+5);
  triangle(width/2+145, height/2-155, width/2+155, height/2-155, width/2+155, height/2-145);
  
  strokeWeight(1);
  line(width/2+10, height/2+10, width/2+15, height/2+5);
  line(width/2+15, height/2+5, width/2+20,height/2+10);
  line(width/2-32-textWidth(" = 1"), height/2-26, width/2-37-textWidth(" = 1"), height/2-21);
  line(width/2-32-textWidth(" = 1"), height/2-26, width/2-27-textWidth(" = 1"), height/2-21);
    strokeWeight(2);
  for (int y = height/2-150; y <= height/2; y+=10)
  {
    // stroke(-(y>>1&4));
    line(width/2+150, y, width/2+150, y+4);
  }
  for(int x = width/2; x <= width/2+150; x+= 10)
  {
    line(x, height/2-150, x+4, height/2-150);
  }
  
  textSize(20);
  text("i = 1", width/2+12, height/2+25);
  text("j = 1", width/2-19-textWidth(" = 1"), height/2-8);
  save("UnitVector.jpg");
}
