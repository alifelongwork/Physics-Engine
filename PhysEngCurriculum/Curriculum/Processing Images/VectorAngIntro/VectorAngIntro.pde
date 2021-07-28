void DrawAxes()
{
  strokeWeight(5);
  line(0, 0, width, 0);
  line(0, 0, 0, height);
}

void setup()
{
  size(400, 400);
  background(255);
  DrawAxes();
  strokeWeight(2);
  line(0, 0, width / 2, height / 2);
  fill(0);
  triangle(width / 2, height / 2 - 10, width / 2  +5, height / 2 + 5, width / 2 - 10, height / 2);
  
  strokeWeight(2);
  for (int y = 0; y <= height/2; y+=10)
  {
    // stroke(-(y>>1&4));
    line(width/2, y, width/2, y+4);
  }
  noFill();
  arc(0,0,125,125,0, radians(45));
  textSize(24);
  text("y", width / 2 + textWidth("y"), height/4);
  text("x", width / 3, textAscent());
  save("VectorAngIntro.jpg");
}
