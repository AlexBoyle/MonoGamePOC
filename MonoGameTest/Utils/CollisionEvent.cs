﻿using System;
using System.Collections.Generic;
namespace MonoGameTest.Utils
{
    public class CollisionEvent
    {
        public Boolean top = false;
        public Boolean left = false;
        public Boolean right = false;
        public Boolean bottom = false;
        public CollisionBox collisionBox;
        public float allignToTop = 0;
        public float allignToBottom = 0;
        public float allignToLeft = 0;
        public float allignToRight = 0;
        public Vector2 depth = Vector2.Zero;
        private List<CollisionEvent> collisionEvents = null;

        // c1 is assumed to be what needs to by resolved
        public CollisionEvent(List<CollisionEvent> collisionEvents)
        {
            this.collisionEvents = collisionEvents;
            Vector2 totalDepth = Vector2.Zero;
            Vector2 totalDepth1 = Vector2.Zero;
            foreach(CollisionEvent collisionEvent in collisionEvents) 
            {
                Vector2 depthSign = new(
                    Math.Abs(totalDepth1.X) < Math.Abs(collisionEvent.depth.X) && totalDepth1.X  != 0? Math.Sign(totalDepth1.X) : Math.Sign(collisionEvent.depth.X),
                    Math.Abs(totalDepth1.Y) < Math.Abs(collisionEvent.depth.Y) && totalDepth1.Y != 0 ? Math.Sign(totalDepth1.Y) : Math.Sign(collisionEvent.depth.Y)
                );
                Debug.Write("Sign: ");
                Debug.WriteLine(depthSign);
                totalDepth += collisionEvent.depth;
                totalDepth1 = new(Math.Abs(totalDepth1.X) + Math.Abs(collisionEvent.depth.X), Math.Abs((totalDepth1.Y)) + Math.Abs(collisionEvent.depth.Y));
                totalDepth1 *= depthSign;
                
                if (collisionEvent.top) { allignToTop = collisionEvent.allignToTop; }
                if (collisionEvent.bottom) { allignToBottom = collisionEvent.allignToBottom; }
                if (collisionEvent.left) { allignToLeft = collisionEvent.allignToLeft; }
                if (collisionEvent.right) { allignToRight = collisionEvent.allignToRight; }
            }
            Debug.Write("Ex Depth:");
            Debug.WriteLine(totalDepth1);
            Debug.Write("Depth:");
            Debug.WriteLine(totalDepth);
            this.depth = totalDepth;
            this.depth = totalDepth1;
            updateCollisionLocations();

        }
        public CollisionEvent(CollisionBox c1, CollisionBox c2)
        {

            this.collisionBox = c2;
            Vector2 centerDiff = c1.origin - c2.origin;
            Vector2 minDiff = new((c1.bounds.Width + c2.bounds.Width) / 2f, (c1.bounds.Height + c2.bounds.Height) / 2f);
            this.depth = new(
                centerDiff.X > 0 ? minDiff.X - centerDiff.X : -minDiff.X - centerDiff.X,
                centerDiff.Y > 0 ? minDiff.Y - centerDiff.Y : -minDiff.Y - centerDiff.Y

            );
            updateCollisionLocations();
            if(top){ allignToTop = c2.getBottomAlignemnt(); }
            if(bottom){ allignToBottom = c2.getTopAlignemnt(); }
            if(left){ allignToLeft = c2.getRightAlignemnt(); }
            if(right){ allignToRight = c2.getLeftAlignemnt(); }
        }
        private void updateCollisionLocations()
        {
            if (depth.X != 0 && depth.Y != 0)
            {
                if (Math.Abs(depth.X) < Math.Abs(depth.Y))
                {
                    if (depth.X > 0) { left = true; }
                    else { right = true; }
                }
                else if (Math.Abs(depth.X) > Math.Abs(depth.Y))
                {
                    if (depth.Y > 0) { top = true; }
                    else { bottom = true; }
                }
                else {
                    if (depth.X > 0) { left = true; }
                    else { right = true; }
                    if (depth.Y > 0) { top = true; }
                    else { bottom = true; }
                }
            }
        }
        public string toString()
        {
            return "{ top: " + top + ", bottom: " + bottom + ", Left: " + left + ", Right: " + right + "}";
        }
    }
}
