using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    int spriteNum;
    [SerializeField] Sprite[] sprites;
    [SerializeField] SpriteRenderer sprite;

    void Update()
    {
        spriteNum = findSprite();
        sprite.sprite = sprites[spriteNum];
    }

    int findSprite() {  
        if (PlayerInputs.direction.magnitude == 0)
        {
            return 0;
        }
        float a = Vector2.Angle(Vector2.left, PlayerInputs.direction);
        if (PlayerInputs.direction.y > 0) {
            if (a < 25) { return checkMagnitude(1); }
            if (a < 65) { return checkMagnitude(2); }
            if (a < 115) { return checkMagnitude(3); }
            if (a < 155) { return checkMagnitude(4); }
            return checkMagnitude(5);
        } else {
            if (a < 25) { return checkMagnitude(1); }
            if (a < 65) { return checkMagnitude(8); }
            if (a < 115) { return checkMagnitude(7); }
            if (a < 155) { return checkMagnitude(6); }
            return checkMagnitude(5);
        }
    }

    int checkMagnitude(int i) {
        if (PlayerInputs.direction.magnitude >= 1)
        {
            return i + 8;
        }
        return i;
    }
}
