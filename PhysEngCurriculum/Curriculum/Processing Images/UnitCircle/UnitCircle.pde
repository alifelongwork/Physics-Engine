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
  text("1", width/2+135, height/2-5);
  text("-1", width/2-160, height/2-5);
  text("1", width/2+5, height/2 - 135);
  text("-1", width/2 + 5, height/2+145);
  strokeWeight(2);
  line(width/2, height/2, width/2+88, height/2-88);
  strokeWeight(4);
  stroke(1);
  noFill();

  pushMatrix();
  translate(width/2, height/2);
  arc(5, 0, 50, 50, -radians(45), radians(0));
  popMatrix();
  strokeWeight(2);
  for (int y = height/2-88; y <= height/2; y+=10)
  {
    // stroke(-(y>>1&4));
    line(width/2+88, y, width/2+88, y+4);
  }
  for(int x = width/2; x <= width/2+88; x+= 10)
  {
    line(x, height/2-88, x+4, height/2-88);
  }
  text("1", width/2+32, height/2-44);
  
  save("UnitCircle.jpg");
}
