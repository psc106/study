using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0615
{
    public class Program
    {
        enum itemType : byte
        {
            a,b,c,d,f,e,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,
            ah, hb,hc,hd,hf,he,hg,hh,hi,hj,hk,hlh,hm,hn,ho,hp,hq,hr,hs,ht,huh,vh,wh,xh,yh,zh,
            cac, cb, ccc, dcc,ccf, ce, cg, cchc, ci, jc, kc, cl, cm, nc, co, cp, cq, cr, cs, ct, cu, cv, cw, cx, cy, cz,
            ach, hcb, hcc,cchd, chf, hec, hgc, hch, chi, hcj, hck, hlch, hbm, hbn, bho, bhp, bhq, bhr, bhs, bht, bhuh,bvh, bwh, bxh, byh, zhb,
            xaab,bxab,cxba,bxda,bxfa,bxea,bxga,bxha,bxia,bxja,bxka,lxba,mxba,nxab,axob,axpb,axqb,rxab,sxab,txab,axbu,axvb,wxba,xba,bya,zxab,
            xaacb,bxcab,ccxba,bcxda,cbxfa,bcxea,bxgca,bxcha,bcxia,bxcja,bxcka,lxbca,mxcba,nxcab,acxob,acxpb,acxqb,rxcab,sxcab,tcxab,axcbu,acxvb,wx3ba,x2ba,byaa,zbxab,
            aab,bab,cba,bda,bfa,bea,bga,bha,bia,bja,bka,lba,mba,nab,aob,apb,aqb,rab,sab,tab,abu,avb,wba,xbca,bcya,zab,
            aa,ba,ca,da,fa,ea,ga,ha,ia,ja,ka,la,ma,na,ao,ap,aq,ra,sa,ta,au,av,wa,xa,ya,za
        }
        static void Main(string[] args)
        {
            itemType it;
            it = itemType.za;
            Console.Write((int)it);
            if ((int)it==0)
            {
                Console.Write((int)it);
            }

            EnumClass c = new EnumClass();

            int a = c.a;
        }
    }

    public class EnumClass
    {
        public int a { get; private set; }
        private int b;
        public int B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;  //예약어
            }
        }
    }
}
