using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0,0,0.25,0.25]")]
	public partial class GunnerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 11;

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
				_dirtyFields[0] |= 0x2;
				_health = value;
				hasDirtyFields = true;
			}
		}

		public void SethealthDirty()
		{
			_dirtyFields[0] |= 0x2;
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
				_dirtyFields[0] |= 0x4;
				_target = value;
				hasDirtyFields = true;
			}
		}

		public void SettargetDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_target(ulong timestep)
		{
			if (targetChanged != null) targetChanged(_target, timestep);
			if (fieldAltered != null) fieldAltered("target", _target, timestep);
		}
		[ForgeGeneratedField]
		private Quaternion _baseRotation;
		public event FieldEvent<Quaternion> baseRotationChanged;
		public InterpolateQuaternion baseRotationInterpolation = new InterpolateQuaternion() { LerpT = 0.25f, Enabled = true };
		public Quaternion baseRotation
		{
			get { return _baseRotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_baseRotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_baseRotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetbaseRotationDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_baseRotation(ulong timestep)
		{
			if (baseRotationChanged != null) baseRotationChanged(_baseRotation, timestep);
			if (fieldAltered != null) fieldAltered("baseRotation", _baseRotation, timestep);
		}
		[ForgeGeneratedField]
		private Quaternion _barrelRotation;
		public event FieldEvent<Quaternion> barrelRotationChanged;
		public InterpolateQuaternion barrelRotationInterpolation = new InterpolateQuaternion() { LerpT = 0.25f, Enabled = true };
		public Quaternion barrelRotation
		{
			get { return _barrelRotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_barrelRotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_barrelRotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetbarrelRotationDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_barrelRotation(ulong timestep)
		{
			if (barrelRotationChanged != null) barrelRotationChanged(_barrelRotation, timestep);
			if (fieldAltered != null) fieldAltered("barrelRotation", _barrelRotation, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			ownerNetworkIdInterpolation.current = ownerNetworkIdInterpolation.target;
			healthInterpolation.current = healthInterpolation.target;
			targetInterpolation.current = targetInterpolation.target;
			baseRotationInterpolation.current = baseRotationInterpolation.target;
			barrelRotationInterpolation.current = barrelRotationInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _ownerNetworkId);
			UnityObjectMapper.Instance.MapBytes(data, _health);
			UnityObjectMapper.Instance.MapBytes(data, _target);
			UnityObjectMapper.Instance.MapBytes(data, _baseRotation);
			UnityObjectMapper.Instance.MapBytes(data, _barrelRotation);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_ownerNetworkId = UnityObjectMapper.Instance.Map<uint>(payload);
			ownerNetworkIdInterpolation.current = _ownerNetworkId;
			ownerNetworkIdInterpolation.target = _ownerNetworkId;
			RunChange_ownerNetworkId(timestep);
			_health = UnityObjectMapper.Instance.Map<float>(payload);
			healthInterpolation.current = _health;
			healthInterpolation.target = _health;
			RunChange_health(timestep);
			_target = UnityObjectMapper.Instance.Map<Vector3>(payload);
			targetInterpolation.current = _target;
			targetInterpolation.target = _target;
			RunChange_target(timestep);
			_baseRotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			baseRotationInterpolation.current = _baseRotation;
			baseRotationInterpolation.target = _baseRotation;
			RunChange_baseRotation(timestep);
			_barrelRotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			barrelRotationInterpolation.current = _barrelRotation;
			barrelRotationInterpolation.target = _barrelRotation;
			RunChange_barrelRotation(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _ownerNetworkId);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _health);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _target);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _baseRotation);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _barrelRotation);

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
			if ((0x4 & readDirtyFlags[0]) != 0)
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
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (baseRotationInterpolation.Enabled)
				{
					baseRotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					baseRotationInterpolation.Timestep = timestep;
				}
				else
				{
					_baseRotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_baseRotation(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (barrelRotationInterpolation.Enabled)
				{
					barrelRotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					barrelRotationInterpolation.Timestep = timestep;
				}
				else
				{
					_barrelRotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_barrelRotation(timestep);
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
			if (baseRotationInterpolation.Enabled && !baseRotationInterpolation.current.UnityNear(baseRotationInterpolation.target, 0.0015f))
			{
				_baseRotation = (Quaternion)baseRotationInterpolation.Interpolate();
				//RunChange_baseRotation(baseRotationInterpolation.Timestep);
			}
			if (barrelRotationInterpolation.Enabled && !barrelRotationInterpolation.current.UnityNear(barrelRotationInterpolation.target, 0.0015f))
			{
				_barrelRotation = (Quaternion)barrelRotationInterpolation.Interpolate();
				//RunChange_barrelRotation(barrelRotationInterpolation.Timestep);
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
