﻿/*
 *  项目名字：MyFrameWork
 *  创建时间：2020.1.21
 *  描述信息：Excel数据处理。
 *  使用说明：
 *  1：写自己需要转换Excel表格的C#代码。可以参考Assets/MyTools/ExcelGameData/GameData/下的代码。
 *  需要热更的脚本放在Assets/MyTools/ExcelGameData/HotFixGameData/
 *  C#脚本也放在该路径下，该路径下只能放需要转换Excel表格
 *  2：我的工具-配置Excel表格-生成默认表格。下面ExcelFileDir即为生成的表格路径。
 *  3：表格填数据。
 *  4：我的工具-配置Excel表格-读取配置表格。
 *  5：MyGameData.InitGameData();调用初始化代码。
 *  6：通过MyGameData.config.可以点出所有的数据。
 */


using OfficeOpenXml;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;

public class CreateExcel : MonoBehaviour {

    /// <summary>
    /// 存放Excel的路径
    /// </summary>
    static string ExcelFileDir = Application.dataPath.Substring(0, Application.dataPath.Length - 7) + "/ConfigExcels/";
    /// <summary>
    /// 需要写成Excel的文件夹地址
    /// </summary>
    static string scriptDir = "Assets/MyTools/ExcelGameData/GameData/";
    /// <summary>
    /// 需要写成Excel的文件夹地址
    /// </summary>
    static string HotFixscriptDir = "Assets/MyTools/ExcelGameData/HotFixGameData/";
    /// <summary>
    /// 文件格式 
    /// 1：.xls
    /// 2：.xlsx
    /// </summary>
    static string FileFormat = ".xlsx";

    [MenuItem("我的工具/其他/配置Excel表格/生成默认表格", false, 1)]
    public static void CreateDefaultExcel()
    {
        if (!Directory.Exists(ExcelFileDir))
            Directory.CreateDirectory(ExcelFileDir);
        if (!Directory.Exists(scriptDir)) 
        {
            Debug.LogError("需要写入表格的文件路径不存在："+scriptDir);
            return;
        }
        DirectoryInfo direction = new DirectoryInfo(scriptDir);
        //把同一个文件夹下的数据写进同一张表，根据sheet划分
        DirectoryInfo[] directions = direction.GetDirectories(); 
        for (int i = 0; i < directions.Length; i++)
        {
            FileInfo[] files = directions[i].GetFiles("*", SearchOption.AllDirectories);
            for (int x = 0; x < files.Length; x++)
            {
                if (files[x].Name.EndsWith(".meta")) continue;
                //写入的文件类名
                string className = files[x].Name.Split('.')[0];
                //储存路径和文件名
                string excelDir = ExcelFileDir + directions[i].Name + FileFormat;
                Type type = Assembly.Load("Assembly-CSharp").GetType(className);
                FieldInfo[] fields = type.GetFields();
                WriteExcel(excelDir, fields, className);                
            }
        }
        CreateHotFixDefaultExcel();
    }
    public static void CreateHotFixDefaultExcel()
    {
        if (!Directory.Exists(ExcelFileDir))
            Directory.CreateDirectory(ExcelFileDir);
        if (!Directory.Exists(HotFixscriptDir))
        {
            Debug.LogError("需要写入表格的文件路径不存在：" + HotFixscriptDir);
            return;
        }
        DirectoryInfo direction = new DirectoryInfo(HotFixscriptDir);
        DirectoryInfo[] directions = direction.GetDirectories();
        for (int i = 0; i < directions.Length; i++)
        {
            FileInfo[] files = directions[i].GetFiles("*", SearchOption.AllDirectories);

            for (int x = 0; x < files.Length; x++)
            {
                if (files[x].Name.EndsWith(".meta")) continue;
                string className = files[x].Name.Split('.')[0];
                string excelDir = ExcelFileDir + directions[i].Name + FileFormat;
                Type type = Assembly.Load("Assembly-CSharp").GetType(className);
                FieldInfo[] fields = type.GetFields();
                WriteExcel(excelDir, fields, className);
            }
        }     
    }

    private static void WriteExcel(string outputDir, FieldInfo[] fieldInfos,string sheetName)
    {
        FileInfo newFile = new FileInfo(outputDir);
        if (!newFile.Exists)
        {
            newFile = new FileInfo(outputDir);
        }
        using (ExcelPackage package = new ExcelPackage(newFile))
        {
          
            //根据Worksheet的名称判断是否写过这个表格         
            for (int i = 0; i < package.Workbook.Worksheets.Count; i++)
            {
                if (package.Workbook.Worksheets[i+1].Name==sheetName)
                {
                    Debug.LogError(sheetName + "表格已存在，跳过，如需替换，需手动删除。");
                    return;
                }
            }
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(sheetName);
            //把每个属性都放进Excel表格    
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = "描述（可替换）";
                worksheet.Cells[2, i + 1].Value = fieldInfos[i].FieldType.ToString();
                worksheet.Cells[3, i + 1].Value = fieldInfos[i].Name;
            }
            //save our new workbook and we are done!        
            package.Save();
            Debug.LogError(sheetName + "表格生成成功");
        }
    }

}



