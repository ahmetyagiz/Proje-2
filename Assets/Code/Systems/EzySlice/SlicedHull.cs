using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EzySlice {

	/**
	 * The final generated data structure from a slice operation. This provides easy access
	 * to utility functions and the final Mesh data for each section of the HULL.
	 */
	public sealed class SlicedHull {
		private Mesh outer_hull;
		private Mesh inner_hull;

		public SlicedHull(Mesh outerHull, Mesh innerHull) {
			this.outer_hull = outerHull;
			this.inner_hull = innerHull;
		}

		public GameObject CreateOuterHull(GameObject original) {
			return CreateOuterHull(original, null);
		}

		public GameObject CreateOuterHull(GameObject original, Material crossSectionMat) {
			GameObject newObject = CreateOuterHull();

			if (newObject != null) {
				newObject.transform.localPosition = original.transform.localPosition;
				newObject.transform.localRotation = original.transform.localRotation;
				newObject.transform.localScale = original.transform.localScale;

				Material[] shared = original.GetComponent<MeshRenderer>().sharedMaterials;
                Mesh mesh = original.GetComponent<MeshFilter>().sharedMesh;

                // nothing changed in the hierarchy, the cross section must have been batched
                // with the submeshes, return as is, no need for any changes
                if (mesh.subMeshCount == outer_hull.subMeshCount) {
                    // the the material information
                    newObject.GetComponent<Renderer>().sharedMaterials = shared;

                    return newObject;
                }

                // otherwise the cross section was added to the back of the submesh array because
                // it uses a different material. We need to take this into account
                Material[] newShared = new Material[shared.Length + 1];

                // copy our material arrays across using native copy (should be faster than loop)
                System.Array.Copy(shared, newShared, shared.Length);
                newShared[shared.Length] = crossSectionMat;

                // the the material information
                newObject.GetComponent<Renderer>().sharedMaterials = newShared;
			}

			return newObject;
		}

		public GameObject CreateInnerrHull(GameObject original) {
			return CreateInnerHull(original, null);
		}

		public GameObject CreateInnerHull(GameObject original, Material crossSectionMat) {
			GameObject newObject = CreateInnerHull();

			if (newObject != null) {
				newObject.transform.localPosition = original.transform.localPosition;
				newObject.transform.localRotation = original.transform.localRotation;
				newObject.transform.localScale = original.transform.localScale;

				Material[] shared = original.GetComponent<MeshRenderer>().sharedMaterials;
                Mesh mesh = original.GetComponent<MeshFilter>().sharedMesh;

                // nothing changed in the hierarchy, the cross section must have been batched
                // with the submeshes, return as is, no need for any changes
                if (mesh.subMeshCount == inner_hull.subMeshCount) {
                    // the the material information
                    newObject.GetComponent<Renderer>().sharedMaterials = shared;

                    return newObject;
                }

                // otherwise the cross section was added to the back of the submesh array because
                // it uses a different material. We need to take this into account
                Material[] newShared = new Material[shared.Length + 1];

                // copy our material arrays across using native copy (should be faster than loop)
                System.Array.Copy(shared, newShared, shared.Length);
                newShared[shared.Length] = crossSectionMat;

                // the the material information
                newObject.GetComponent<Renderer>().sharedMaterials = newShared;
			}

			return newObject;
		}

        /**
         * Generate a new GameObject from the upper hull of the mesh
         * This function will return null if upper hull does not exist
         */
        public GameObject CreateOuterHull() {
            return CreateEmptyObject("Outer_Hull", outer_hull);
        }

		/**
		 * Generate a new GameObject from the Lower hull of the mesh
		 * This function will return null if lower hull does not exist
		 */
		public GameObject CreateInnerHull() {
			return CreateEmptyObject("Inner_Hull", inner_hull);
		}

		public Mesh upperHull {
			get { return this.outer_hull; }
		}

		public Mesh lowerHull {
			get { return this.inner_hull; }
		}

		/**
		 * Helper function which will create a new GameObject to be able to add
		 * a new mesh for rendering and return.
		 */
		private static GameObject CreateEmptyObject(string name, Mesh hull) {
			if (hull == null) {
				return null;
			}

			GameObject newObject = new GameObject(name);

			newObject.AddComponent<MeshRenderer>();
			MeshFilter filter = newObject.AddComponent<MeshFilter>();

			filter.mesh = hull;

			return newObject;
		}
	}
}