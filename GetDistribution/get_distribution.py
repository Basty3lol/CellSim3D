import numpy as np
import seaborn as sns
import matplotlib.pyplot as plt
import pandas as pd
from scipy.stats import gaussian_kde
import os

df = pd.read_csv("StatisticsSCerevisiae.csv")
df.head()

def dist_norm_ecoli():
    Data = []
    S = np.random.normal(22.5043, 4.6321, 1000000)
    V = sns.kdeplot(data=S)

    data = V.lines[0].get_data()
    ac = []
    s = 0

    for j in range(len(data[0])):
        s += data[1][j]/sum(data[1])
        ac.append(s)

    V2 = plt.plot(data[0],ac)
    Data.append((ac,data[0]))
    
    time_phase = Data[0]
    print(len(time_phase[0]),len(time_phase[1]))
    arr = np.transpose([time_phase[0],time_phase[1]])
    df2 = pd.DataFrame(arr, columns =['Var', 'Min'])
    df2.to_csv('EcoliGrowthTimeDensity.csv', float_format='%.15f')

    #Mostrar imagen de distribucion
    # plt.show()
    # plt.clf()
    # plt.title("Distribución E. Coli")
    # plt.plot(data[0],data[1])
    # plt.plot(data[0],ac)
    # plt.legend(["Distribución",'Distribución Acumulada'])
    # plt.xlabel("Time")
    # plt.ylabel("Probability")
    # plt.show()

    return Data
    

def dist_scerev(name):
    f = []
    c = 0

    for i in df:
        if((name in i) and (c < 12)):
            c += 1
            f.append(i)

    Data = []
    
    for i in f:
        V = None
        Min = 0
        Max = 0
        if("nseg"in i):
            Min = min(df[i])
            Max = 12
        elif("Duration" in i):
            Min = 0
            Max = 200
        else:
            Min = min(df[i])-1 
            Max = max(df[i])+1

        if(len(df[i].dropna()) < 2):
            V = sns.kdeplot(data=df[i], clip=(Min,Max))
        else:
            V = sns.kdeplot(data=df[i], clip=(Min,Max))
        
        data = V.lines[0].get_data()
        ac = []
        s = 0

        for j in range(len(data[0])):
            s += data[1][j]/sum(data[1])
            ac.append(s)

        V2 = plt.plot(data[0],ac)
        plt.legend(['Densidad De Datos',"Probabilidad Acumulada"])
        Data.append((ac,data[0]))
        
        time_phase = Data[0]
        arr = np.transpose([time_phase[0],time_phase[1]])
        df2 = pd.DataFrame(arr, columns =['Var', 'Min'])
        df2.to_csv('SCerevGrowthTimeDensity.csv', float_format='%.15f')

        #Guardar imagen y mostrar
        # plt.savefig("./"+name+"/"+str(i)+".png")
        # plt.show()
        # plt.clf()
        # plt.title("Distribución S. Cerevisiae")
        # plt.plot(data[0],data[1]*5)
        # plt.plot(data[0],ac)
        # plt.legend(["Distribución",'Distribución Acumulada'])
        # plt.xlabel("Time")
        # plt.ylabel("Probability")
        # plt.show()

    return Data

#dist_norm_ecoli()
dist_scerev("phaseDuration")