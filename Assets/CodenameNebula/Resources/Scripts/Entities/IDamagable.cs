﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IDamagable
{
    void TakeDamage(float damage);
    void Die();
    void RegenHP(float dt);
}

