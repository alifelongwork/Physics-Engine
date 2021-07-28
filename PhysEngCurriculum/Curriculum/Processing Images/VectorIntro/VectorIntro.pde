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
  save("VectorIntro.jpg");
}
