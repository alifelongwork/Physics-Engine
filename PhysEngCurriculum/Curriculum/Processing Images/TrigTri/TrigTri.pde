void drawAxes()
{
  strokeWeight(5);
  line(0, 0, width, 0);
  line(0, 0, 0, height);
  text("+x-axis", width - textWidth("+x-axis"), 25);
  pushMatrix();
  translate(0, height - textWidth("+y-axis"));
  rotate(PI/2);
  text("+y-axis", 0, -15);
  popMatrix();
}
void setup()
{
  size(400, 400);
  background(255);
  drawAxes();
  strokeWeight(2);
  triangle(0, 0, width / 2, 0, width / 2, height / 2);
  fill(0);
  textSize(24);
  text("x", width / 4, 20);
  text("y", width / 2 + 5, height / 4);
  noFill();
  arc(0, 0, 100, 100, 0, radians(45));
  text("r", width / 4, height / 4);
}

void draw()
{
  drawAxes();
  save("TrigTri.jpg");
}
