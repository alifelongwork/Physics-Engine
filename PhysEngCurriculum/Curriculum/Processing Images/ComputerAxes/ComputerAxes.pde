int rectSize=15;
int space=1;

void setup() {
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
  fill(0);
  textSize(24);
  stroke(0);
  strokeWeight(5);
  line(0, 0, width, 0);
  line(0, 0, 0, height);
  text("+x-axis", width - textWidth("+x-axis"), 25);
  pushMatrix();
  translate(0, height - textWidth("+y-axis"));
  rotate(PI / 2);
  text("+y-axis", 0, -15);
  popMatrix();
  text("0, 0", 0, 20);
  save("ComputerAxes.jpg");
}

void draw()
{

  //strokeWeight(2);
  // line(0, 0, width / 2, height / 2);

  //pushMatrix();
  //translate(width/2, height/2);
  //triangle(5, -5, 5, 5, -5, 5);
  //popMatrix();
  //fill(0);
  //textSize(24);
  //text("A", width / 2, height / 2 - 15);
}
