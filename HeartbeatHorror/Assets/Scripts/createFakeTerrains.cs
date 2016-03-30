using UnityEditor;
using UnityEngine;

public class createFakeTerrains {

	static void CopyAllTerrainData(Terrain src, Terrain dst) {
		//copy all data over as is for now, maybe tweak later
		dst.bakeLightProbesForTrees = src.bakeLightProbesForTrees;
		dst.basemapDistance = src.basemapDistance;
		dst.castShadows = src.castShadows;
		dst.collectDetailPatches = src.collectDetailPatches;
		dst.detailObjectDensity = src.detailObjectDensity;
		dst.detailObjectDistance = src.detailObjectDistance;
		dst.drawHeightmap = src.drawHeightmap;
		dst.drawTreesAndFoliage = src.drawTreesAndFoliage;
		dst.heightmapMaximumLOD = src.heightmapMaximumLOD;
		dst.heightmapPixelError = src.heightmapPixelError;
		dst.legacyShininess = src.legacyShininess;
		dst.legacySpecular = src.legacySpecular;
		dst.lightmapIndex = src.lightmapIndex;
		dst.lightmapScaleOffset = src.lightmapScaleOffset;
		dst.materialTemplate = src.materialTemplate;
		dst.materialType = src.materialType;
		dst.realtimeLightmapIndex = src.realtimeLightmapIndex;
		dst.realtimeLightmapScaleOffset = src.realtimeLightmapScaleOffset;
		dst.reflectionProbeUsage = src.reflectionProbeUsage;
		dst.treeBillboardDistance = src.treeBillboardDistance;
		dst.treeCrossFadeLength = src.treeCrossFadeLength;
		dst.treeDistance = src.treeDistance;
		dst.treeMaximumFullLODCount = src.treeMaximumFullLODCount;

	}

	[MenuItem("TerrainTools/CreateFakeTerrains")]
	static void Create() {
		Transform tform = Selection.activeGameObject.transform;
		Terrain t = Selection.activeGameObject.GetComponent<Terrain>();
		TerrainData td = t.terrainData;

		float offset = td.size.x;// / 2;

		GameObject tmp;

		tmp = Terrain.CreateTerrainGameObject(td);
		tmp.name = Selection.activeGameObject.name + " (Copy)";
		tmp.transform.position = new Vector3(tform.position.x + offset, tform.position.y, tform.position.z + offset);
		CopyAllTerrainData(t,tmp.GetComponent<Terrain>());

		tmp = Terrain.CreateTerrainGameObject(td);
		tmp.name = Selection.activeGameObject.name + " (Copy)";
		tmp.transform.position = new Vector3(tform.position.x + offset, tform.position.y, tform.position.z - offset);
		CopyAllTerrainData(t, tmp.GetComponent<Terrain>());

		tmp = Terrain.CreateTerrainGameObject(td);
		tmp.name = Selection.activeGameObject.name + " (Copy)";
		tmp.transform.position = new Vector3(tform.position.x + offset, tform.position.y, tform.position.z);
		CopyAllTerrainData(t, tmp.GetComponent<Terrain>());

		tmp = Terrain.CreateTerrainGameObject(td);
		tmp.name = Selection.activeGameObject.name + " (Copy)";
		tmp.transform.position = new Vector3(tform.position.x - offset, tform.position.y, tform.position.z + offset);
		CopyAllTerrainData(t, tmp.GetComponent<Terrain>());

		tmp = Terrain.CreateTerrainGameObject(td);
		tmp.name = Selection.activeGameObject.name + " (Copy)";
		tmp.transform.position = new Vector3(tform.position.x - offset, tform.position.y, tform.position.z - offset);
		CopyAllTerrainData(t, tmp.GetComponent<Terrain>());

		tmp = Terrain.CreateTerrainGameObject(td);
		tmp.name = Selection.activeGameObject.name + " (Copy)";
		tmp.transform.position = new Vector3(tform.position.x - offset, tform.position.y, tform.position.z);
		CopyAllTerrainData(t, tmp.GetComponent<Terrain>());

		tmp = Terrain.CreateTerrainGameObject(td);
		tmp.name = Selection.activeGameObject.name + " (Copy)";
		tmp.transform.position = new Vector3(tform.position.x, tform.position.y, tform.position.z + offset);
		CopyAllTerrainData(t, tmp.GetComponent<Terrain>());

		tmp = Terrain.CreateTerrainGameObject(td);
		tmp.name = Selection.activeGameObject.name + " (Copy)";
		tmp.transform.position = new Vector3(tform.position.x, tform.position.y, tform.position.z - offset);
		CopyAllTerrainData(t, tmp.GetComponent<Terrain>());

	}

	
}
