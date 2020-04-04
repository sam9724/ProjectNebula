using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0.15,0,0]")]
	public partial class GunnerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 4;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private uint _ownerNetworkId;
		public event FieldEvent<uint> ownerNetworkIdChanged;
		public Interpolated<uint> ownerNetworkIdInterpolation = new Interpolated<uint>() { LerpT = 0f, Enabled = false };
		public uint ownerNetworkId
		{
			get { return _ownerNetworkId; }
			set
			{
				// Don't do anything if the value is the same
				if (_ownerNetworkId == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_ownerNetworkId = value;
				hasDirtyFields = true;
			}
		}

		public void SetownerNetworkIdDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_ownerNetworkId(ulong timestep)
		{
			if (ownerNetworkIdChanged != null) ownerNetworkIdChanged(_ownerNetworkId, timestep);
			if (fieldAltered != null) fieldAltered("ownerNetworkId", _ownerNetworkId, timestep);
		}
		[ForgeGeneratedField]
		private Quaternion _rotation;
		public event FieldEvent<Quaternion> rotationChanged;
		public InterpolateQuaternion rotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion rotation
		{
			get { return _rotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_rotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_rotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetrotationDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_rotation(ulong timestep)
		{
			if (rotationChanged != null) rotationChanged(_rotation, timestep);
			if (fieldAltered != null) fieldAltered("rotation", _rotation, timestep);
		}
		[ForgeGeneratedField]
		private float _health;
		public event FieldEvent<float> healthChanged;
		public InterpolateFloat healthInterpolation = new InterpolateFloat() { LerpT = 0f, Enabled = false };
		public float health
		{
			get { return _health; }
			set
			{
				// Don't do anything if the value is the same
				if (_health == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_health = value;
				hasDirtyFields = true;
			}
		}

		public void SethealthDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_health(ulong timestep)
		{
			if (healthChanged != null) healthChanged(_health, timestep);
			if (fieldAltered != null) fieldAltered("health", _health, timestep);
		}
		[ForgeGeneratedField]
		private Vector3 _target;
		public event FieldEvent<Vector3> targetChanged;
		public InterpolateVector3 targetInterpolation = new InterpolateVector3() { LerpT = 0f, Enabled = false };
		public Vector3 target
		{
			get { return _target; }
			set
			{
				// Don't do anything if the value is the same
				if (_target == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_target = value;
				hasDirtyFields = true;
			}
		}

		public void SettargetDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_target(ulong timestep)
		{
			if (targetChanged != null) targetChanged(_target, timestep);
			if (fieldAltered != null) fieldAltered("target", _target, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			ownerNetworkIdInterpolation.current = ownerNetworkIdInterpolation.target;
			rotationInterpolation.current = rotationInterpolation.target;
			healthInterpolation.current = healthInterpolation.target;
			targetInterpolation.current = targetInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _ownerNetworkId);
			UnityObjectMapper.Instance.MapBytes(data, _rotation);
			UnityObjectMapper.Instance.MapBytes(data, _health);
			UnityObjectMapper.Instance.MapBytes(data, _target);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_ownerNetworkId = UnityObjectMapper.Instance.Map<uint>(payload);
			ownerNetworkIdInterpolation.current = _ownerNetworkId;
			ownerNetworkIdInterpolation.target = _ownerNetworkId;
			RunChange_ownerNetworkId(timestep);
			_rotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			rotationInterpolation.current = _rotation;
			rotationInterpolation.target = _rotation;
			RunChange_rotation(timestep);
			_health = UnityObjectMapper.Instance.Map<float>(payload);
			healthInterpolation.current = _health;
			healthInterpolation.target = _health;
			RunChange_health(timestep);
			_target = UnityObjectMapper.Instance.Map<Vector3>(payload);
			targetInterpolation.current = _target;
			targetInterpolation.target = _target;
			RunChange_target(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _ownerNetworkId);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _rotation);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _health);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _target);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (ownerNetworkIdInterpolation.Enabled)
				{
					ownerNetworkIdInterpolation.target = UnityObjectMapper.Instance.Map<uint>(data);
					ownerNetworkIdInterpolation.Timestep = timestep;
				}
				else
				{
					_ownerNetworkId = UnityObjectMapper.Instance.Map<uint>(data);
					RunChange_ownerNetworkId(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (rotationInterpolation.Enabled)
				{
					rotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					rotationInterpolation.Timestep = timestep;
				}
				else
				{
					_rotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_rotation(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (healthInterpolation.Enabled)
				{
					healthInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					healthInterpolation.Timestep = timestep;
				}
				else
				{
					_health = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_health(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (targetInterpolation.Enabled)
				{
					targetInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					targetInterpolation.Timestep = timestep;
				}
				else
				{
					_target = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_target(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (ownerNetworkIdInterpolation.Enabled && !ownerNetworkIdInterpolation.current.UnityNear(ownerNetworkIdInterpolation.target, 0.0015f))
			{
				_ownerNetworkId = (uint)ownerNetworkIdInterpolation.Interpolate();
				//RunChange_ownerNetworkId(ownerNetworkIdInterpolation.Timestep);
			}
			if (rotationInterpolation.Enabled && !rotationInterpolation.current.UnityNear(rotationInterpolation.target, 0.0015f))
			{
				_rotation = (Quaternion)rotationInterpolation.Interpolate();
				//RunChange_rotation(rotationInterpolation.Timestep);
			}
			if (healthInterpolation.Enabled && !healthInterpolation.current.UnityNear(healthInterpolation.target, 0.0015f))
			{
				_health = (float)healthInterpolation.Interpolate();
				//RunChange_health(healthInterpolation.Timestep);
			}
			if (targetInterpolation.Enabled && !targetInterpolation.current.UnityNear(targetInterpolation.target, 0.0015f))
			{
				_target = (Vector3)targetInterpolation.Interpolate();
				//RunChange_target(targetInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public GunnerNetworkObject() : base() { Initialize(); }
		public GunnerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public GunnerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
