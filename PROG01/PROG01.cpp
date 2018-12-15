// PROG01.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <stdlib.h>

int main()
{
	int mem;
	void * pMem[9096];
	int total = 0;

	while (1)
	{
		printf("MEM(KB) : ");
		scanf("%d", &mem); 

		if (mem > 0)
		{
			for (int i = 0; i < mem; i++)
			{
				pMem[total++] = malloc(1024);
			}
		}
		else if (mem < 0)
		{
			for (int i = 0; i < mem*(-1); i++)
			{
				total--;
				if (total < 0)
				{
					total = 0;
					break;
				}
				free(pMem[total]);
			}
		}
		else
		{
			printf("END!\n");
			break;
		}
	}
    return 0;
}

