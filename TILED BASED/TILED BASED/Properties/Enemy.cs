using System;


namespace TILED_BASED
{
    public class Enemy
    {
        private int x;
        private int y;
        private int speed;
        private int leftLimit;
        private int rightLimit;

        public Enemy(int startX, int startY, int enemySpeed, int enemyLeftLimit, int enemyRightLimit)
        {
            x = startX;
            y = startY;
            speed = enemySpeed;
            leftLimit = enemyLeftLimit;
            rightLimit = enemyRightLimit;
        }

        public void Move()
        {
            // Move the enemy to the right
            x += speed;

            // If the enemy has reached its right limit, move it to the left
            if (x >= rightLimit)
            {
                speed = -speed;
            }
            // If the enemy has reached its left limit, move it to the right
            else if (x <= leftLimit)
            {
                speed = -speed;
            }
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }
    }

}


