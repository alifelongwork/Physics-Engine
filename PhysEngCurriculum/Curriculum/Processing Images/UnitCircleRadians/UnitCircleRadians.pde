void drawAxes()
{
  strokeWeight(5);
  line(width / 2, 0, width/2, height);
  line(0, height/2, width, height/2);
}
void setup()
{
  size(400, 400);
  background(255);
  //strokeWeight(3);
  ellipse(width/2, height/2, width/2+50, height/2+50);
  drawAxes();
  fill(0);
  textSize(20);
  line(width/2, height/2, width/2 + 88, height/2-88);
  line(width/2, height/2, width/2-88, height/2-88);
  line(width/2, height/2, width/2-88, height/2+88);
  line(width/2, height/2, width/2+88, height/2+88);

  save("UnitCircleRadians.jpg");
}
