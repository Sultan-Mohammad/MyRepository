﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Entity
{



    public enum Direction
    {
        up = 0,
        down = 1,
        left = 2,
        right = 3
    }


    public class Car
    {

        public List<Rectangle> colliders;

        Point pos;
           
        Rectangle carBody;
        Rectangle carGlass;
        Rectangle carRoof;
        
        ///
        Point[] bodyPontsH;
        Point[] windowPointsH;
        Point[] inWindow1;
        Point[] inWindow2;
        Rectangle wheelL = new Rectangle(0,0,20,20);
        Rectangle wheelR = new Rectangle(0, 0, 20, 20);
        Rectangle outWheelL = new Rectangle(0, 0, 10, 10);
        Rectangle outWheelR= new Rectangle(0, 0, 10, 10);
        Rectangle headLightR = new Rectangle(0, 0, 10, 10);
        Rectangle headLightL = new Rectangle(0, 0, 10, 10);
        Rectangle backlightR = new Rectangle(0, 0, 10, 10);

        Rectangle backlightL = new Rectangle(0, 0, 10, 10);

        Rectangle collisionBoxH = new Rectangle() ; //left right collision
        Rectangle collisionBoxV = new Rectangle() ;

        Direction carDir;
        private Form screenForm;
        private System.Timers.Timer animTimer;
        private float animTimerDelay = 50;
        private int speed=0;

        public Car(Form form)
        {
            pos.X = form.Width / 2;
            pos.Y = form.Height / 2;

           // width = w;
           // height = h;
            carDir = Direction.right;
            screenForm = form;

            initTimers();
            startTimer();

            collisionBoxH = new Rectangle(pos.X,pos.Y, 100,60);
            collisionBoxV = new Rectangle(pos.X, pos.Y, 50,90);


        }



        private void initTimers()
        {
            animTimer = new System.Timers.Timer(animTimerDelay);
            animTimer.Elapsed += new System.Timers.ElapsedEventHandler(timerCallback);

        }

        private void startTimer()
        {
            animTimer.Enabled = true;
            animTimer.Start();
        }


        public void setDirection(Direction dir)
        {
            carDir = dir;
        }

        public void setSpeed(int speed)
        {
            this.speed = speed;
        }

        public void DrawCar(Pen p, Graphics g)
        {


            switch (carDir)
            {
                case Direction.right:

                    collisionBoxH.X = pos.X - collisionBoxH.Width / 2 + speed; // to set the box position at center
                    collisionBoxH.Y = pos.Y - collisionBoxH.Height / 2;
                   

                    if(!collides(collisionBoxH))
                        pos.X += speed;
                    
                    bodyPontsH = new Point[4] { new Point(pos.X - 40, pos.Y - 15), new Point(pos.X - 50, pos.Y + 15), new Point(pos.X + 50, pos.Y + 15), new Point(pos.X + 40, pos.Y - 15) };//, new Point(pos.X - 30, pos.Y - 10) };
                    

                    
                    windowPointsH = new Point[4]{ new Point(pos.X - 30, pos.Y - 35), new Point(pos.X - 35, pos.Y - 15), new Point(pos.X + 25, pos.Y - 15), new Point(pos.X + 20, pos.Y - 35) };




                    inWindow1 = new Point[4] { new Point(pos.X - 8, pos.Y - 25), new Point(pos.X - 12, pos.Y - 5), new Point(pos.X + 8, pos.Y - 5), new Point(pos.X + 10, pos.Y - 25) };

                  // inWindow2 = new Point[4] { new Point(pos.X - 1, pos.Y - 25), new Point(pos.X - 6, pos.Y - 5), new Point(pos.X + 30, pos.Y - 5), new Point(pos.X + 35, pos.Y - 25) };

                    wheelL.X = pos.X - 20 - 10;
                    wheelL.Y = pos.Y + 15 - 10;

                    wheelR.X = pos.X + 20 - 10;
                    wheelR.Y = pos.Y + 15 - 10;


                   
                    outWheelL.X = pos.X - 20 - 5;
                    outWheelL.Y = pos.Y + 10 - 2;

                    outWheelR.X = pos.X + 20 - 5;
                    outWheelR.Y = pos.Y + 10 - 2;


                    headLightR.X = pos.X + 40;
                    headLightR.Y = pos.Y;
                    backlightR.X = pos.X - 50;
                    backlightR.Y = pos.Y;


                    break;

                case Direction.left:
                   
                    collisionBoxH.X = pos.X - collisionBoxH.Width / 2 - speed;
                    collisionBoxH.Y = pos.Y - collisionBoxH.Height / 2;
                   
                    
                    if (!collides(collisionBoxH))
                        pos.X -= speed;
                    bodyPontsH = new Point[4] { new Point(pos.X - 40, pos.Y - 15), new Point(pos.X - 50, pos.Y + 15), new Point(pos.X + 50, pos.Y + 15), new Point(pos.X + 40, pos.Y - 15) };//, new Point(pos.X - 30, pos.Y - 10) };
                    windowPointsH = new Point[4] { new Point(pos.X - 25, pos.Y - 35), new Point(pos.X - 35, pos.Y - 15), new Point(pos.X + 25, pos.Y - 15), new Point(pos.X + 20, pos.Y - 35) };

                    inWindow1 = new Point[4] { new Point(pos.X - 8, pos.Y - 25), new Point(pos.X - 12, pos.Y - 5), new Point(pos.X + 8, pos.Y - 5), new Point(pos.X + 10, pos.Y - 25) };
                   // inWindow2 = new Point[4] { new Point(pos.X - 1, pos.Y - 25), new Point(pos.X - 6, pos.Y - 5), new Point(pos.X + 30, pos.Y - 5), new Point(pos.X + 35, pos.Y - 25) };
                    wheelL.X = pos.X - 20 - 10;
                    wheelL.Y = pos.Y + 15 - 10;

                    wheelR.X = pos.X + 20 - 10;
                    wheelR.Y = pos.Y + 15 - 10;


                    outWheelL.X = pos.X - 20 - 5;
                    outWheelL.Y = pos.Y + 10 - 2;

                    outWheelR.X = pos.X + 20 - 5;
                    outWheelR.Y = pos.Y + 10 - 2;




                    headLightR.X = pos.X - 50;
                    headLightR.Y = pos.Y;

                     backlightR.X = pos.X +40;
                    backlightR.Y = pos.Y;


                    break;
                case Direction.up:

                    collisionBoxV.X = pos.X - collisionBoxV.Width / 2;
                    collisionBoxV.Y = pos.Y - collisionBoxV.Height / 2 - speed;
                   

                    if (!collides(collisionBoxV))
                        pos.Y -= speed;

                    carBody = new Rectangle(pos.X - 25,  pos.Y - 45, 50, 90);
                    carGlass = new Rectangle(pos.X - 22, pos.Y - 15, 44, 45);
                    carRoof = new Rectangle(pos.X - 25, pos.Y - 2, 50, 20);

                    headLightR.X = pos.X + 18 - headLightR.Width/2;
                    headLightR.Y = pos.Y - 45;

                    headLightL.X = pos.X - 18- headLightR.Width / 2;
                    headLightL.Y = pos.Y - 45;

                    


                    break;
                case Direction.down:
    
                    collisionBoxV.X = pos.X - collisionBoxV.Width / 2;
                    collisionBoxV.Y = pos.Y - collisionBoxV.Height / 2 + speed;
                   

                    if (!collides(collisionBoxV))
                        pos.Y += speed;
                    
                    carBody = new Rectangle(pos.X - 25,  pos.Y - 45, 50, 90);
                    carGlass = new Rectangle(pos.X - 22, pos.Y - 30, 44, 45);
                    carRoof = new Rectangle(pos.X - 25, pos.Y -18, 50, 20);

                    headLightR.X = pos.X + 18 - headLightR.Width / 2;
                    headLightR.Y = pos.Y + 45-headLightR.Height;

                    headLightL.X = pos.X - 18 - headLightR.Width / 2;
                    headLightL.Y = pos.Y + 45-headLightR.Height;

                    
                    break;

            }


            if (carDir == Direction.left || carDir == Direction.right)
            {
                p.Color = Color.CornflowerBlue;
                p.Width = 7;
                //g.FillRectangle(p.Brush, carBody);
                g.FillPolygon(p.Brush, bodyPontsH);
                p.Color = Color.CornflowerBlue;
                g.FillPolygon(p.Brush, windowPointsH);

                p.Color = Color.Black;
                g.FillEllipse(p.Brush, wheelL);
                g.FillEllipse(p.Brush, wheelR);


                

                p.Color = Color.LightGray;
                g.FillEllipse(p.Brush, outWheelL);
                g.FillEllipse(p.Brush, outWheelR);


                p.Color = Color.Yellow;
                g.FillEllipse(p.Brush, headLightR);
                g.FillEllipse(p.Brush, backlightR);
                p.Color = Color.LightBlue;
                g.FillPolygon(p.Brush, inWindow1);
             //   g.FillPolygon(p.Brush, inWindow2);


            
            }
            else if(carDir == Direction.up || carDir == Direction.down)
            {
                p.Color = Color.CornflowerBlue;
                g.FillRectangle(p.Brush, carBody);



                p.Color = Color.Blue;
                g.FillRectangle(p.Brush, carGlass);

                p.Color = Color.CornflowerBlue;
                g.FillRectangle(p.Brush, carRoof);

                p.Color = Color.Yellow;
                g.FillEllipse(p.Brush, headLightL);

                p.Color = Color.Yellow;
                g.FillEllipse(p.Brush, headLightR);




            
            }
        }

        private void timerCallback(object sender, System.Timers.ElapsedEventArgs e)
        {
            screenForm.Invalidate();  //redraws form; 

        }


        private bool collides( Rectangle area )
        {

            foreach(Rectangle r in colliders) //checks if the elements of the list intersect eachother or not
            {
                if (area.IntersectsWith(r))
                    return true;
            }

            return false;
        }


    }
}
