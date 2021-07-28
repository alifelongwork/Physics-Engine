int rectSize = 10;
int space = 1;
void setup()
{
  size(400, 400);
  background(255);
  stroke(200);
  noFill();
  strokeWeight(2);

  for (int x = 0; x < width - rectSize; x++) 
  {
    for (int y = 0; y < height - rectSize; y++) 
    {
      rect(space + x * (rectSize + space), space + y *(rectSize + space), rectSize, rectSize);
    }
  }
  stroke(0);
  strokeWeight(5);
  line(width/2, 0, width/2, height);
  line(0, height/2, width, height/2);
  textSize(24);
  fill(0);
  text("x-axis", width - textWidth("x-axis"), height / 2 + 25);
  pushMatrix();
  translate(width/2, height);
  rotate(PI/2);
  text("y-axis", -textWidth("y-axis"), -15);
  popMatrix();
  text("0,0", width/2+5, height/2 + 20);
  save("ConventionalCoordinates.jpg");
}
