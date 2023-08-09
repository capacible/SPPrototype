// <auto-generated />
#pragma warning disable 1570, 1591

using System;
using Microsoft.ML.Probabilistic;
using Microsoft.ML.Probabilistic.Distributions;
using Microsoft.ML.Probabilistic.Math;
using Microsoft.ML.Probabilistic.Collections;
using Microsoft.ML.Probabilistic.Factors;

namespace Models
{
	/// <summary>
	/// Generated algorithm for performing inference.
	/// </summary>
	/// <remarks>
	/// If you wish to use this class directly, you must perform the following steps:
	/// 1) Create an instance of the class.
	/// 2) Set the value of any externally-set fields e.g. data, priors.
	/// 3) Call the Execute(numberOfIterations) method.
	/// 4) Use the XXXMarginal() methods to retrieve posterior marginals for different variables.
	/// 
	/// Generated by Infer.NET 0.4.2301.301 at 5:45 PM on Wednesday, August 9, 2023.
	/// </remarks>
	public partial class DirectorTraining_EP : IGeneratedAlgorithm
	{
		#region Fields
		/// <summary>Field backing the AllEvents property</summary>
		private int[] AllEvents_field;
		/// <summary>Message to marginal of 'AllEvents'</summary>
		public DistributionRefArray<Discrete,int> AllEvents_marginal_F;
		/// <summary>Field backing the AllRels property</summary>
		private int[] AllRels_field;
		/// <summary>Message to marginal of 'AllRels'</summary>
		public DistributionRefArray<Discrete,int> AllRels_marginal_F;
		/// <summary>Field backing the AllTraits property</summary>
		private int[] AllTraits_field;
		/// <summary>Message to marginal of 'AllTraits'</summary>
		public DistributionRefArray<Discrete,int> AllTraits_marginal_F;
		/// <summary>True if Changed_AllEvents_AllRels_AllTraits_DialogueChild_DialogueCPTPrior_NumOfCases has executed. Set this to false to force re-execution of Changed_AllEvents_AllRels_AllTraits_DialogueChild_DialogueCPTPrior_NumOfCases</summary>
		public bool Changed_AllEvents_AllRels_AllTraits_DialogueChild_DialogueCPTPrior_NumOfCases_isDone;
		/// <summary>True if Changed_AllEvents_EventsPriors_NumOfCases has executed. Set this to false to force re-execution of Changed_AllEvents_EventsPriors_NumOfCases</summary>
		public bool Changed_AllEvents_EventsPriors_NumOfCases_isDone;
		/// <summary>True if Changed_AllEvents_NumOfCases has executed. Set this to false to force re-execution of Changed_AllEvents_NumOfCases</summary>
		public bool Changed_AllEvents_NumOfCases_isDone;
		/// <summary>True if Changed_AllRels_NumOfCases has executed. Set this to false to force re-execution of Changed_AllRels_NumOfCases</summary>
		public bool Changed_AllRels_NumOfCases_isDone;
		/// <summary>True if Changed_AllRels_NumOfCases_RelStatusPriors has executed. Set this to false to force re-execution of Changed_AllRels_NumOfCases_RelStatusPriors</summary>
		public bool Changed_AllRels_NumOfCases_RelStatusPriors_isDone;
		/// <summary>True if Changed_AllTraits_NumOfCases has executed. Set this to false to force re-execution of Changed_AllTraits_NumOfCases</summary>
		public bool Changed_AllTraits_NumOfCases_isDone;
		/// <summary>True if Changed_AllTraits_NumOfCases_TraitsPriors has executed. Set this to false to force re-execution of Changed_AllTraits_NumOfCases_TraitsPriors</summary>
		public bool Changed_AllTraits_NumOfCases_TraitsPriors_isDone;
		/// <summary>True if Changed_DialogueChild_NumOfCases has executed. Set this to false to force re-execution of Changed_DialogueChild_NumOfCases</summary>
		public bool Changed_DialogueChild_NumOfCases_isDone;
		/// <summary>True if Changed_DialogueCPTPrior has executed. Set this to false to force re-execution of Changed_DialogueCPTPrior</summary>
		public bool Changed_DialogueCPTPrior_isDone;
		/// <summary>True if Changed_EventsPriors has executed. Set this to false to force re-execution of Changed_EventsPriors</summary>
		public bool Changed_EventsPriors_isDone;
		/// <summary>True if Changed_NumOfCases has executed. Set this to false to force re-execution of Changed_NumOfCases</summary>
		public bool Changed_NumOfCases_isDone;
		/// <summary>True if Changed_RelStatusPriors has executed. Set this to false to force re-execution of Changed_RelStatusPriors</summary>
		public bool Changed_RelStatusPriors_isDone;
		/// <summary>True if Changed_TraitsPriors has executed. Set this to false to force re-execution of Changed_TraitsPriors</summary>
		public bool Changed_TraitsPriors_isDone;
		/// <summary>Field backing the DialogueChild property</summary>
		private int[] DialogueChild_field;
		/// <summary>Message to marginal of 'DialogueChild'</summary>
		public DistributionRefArray<Discrete,int> DialogueChild_marginal_F;
		/// <summary>Message to marginal of 'DialogueCPT'</summary>
		public DistributionRefArray<DistributionRefArray<DistributionRefArray<Dirichlet,Vector>,Vector[]>,Vector[][]> DialogueCPT_marginal_F;
		/// <summary>Field backing the DialogueCPTPrior property</summary>
		private Dirichlet[][][] DialogueCPTPrior_field;
		/// <summary>Message to marginal of 'DialogueCPTPrior'</summary>
		public PointMass<Dirichlet[][][]> DialogueCPTPrior_marginal_F;
		/// <summary>Field backing the EventsPriors property</summary>
		private Dirichlet EventsPriors_field;
		/// <summary>Message to marginal of 'EventsPriors'</summary>
		public PointMass<Dirichlet> EventsPriors_marginal_F;
		/// <summary>Field backing the NumberOfIterationsDone property</summary>
		private int numberOfIterationsDone;
		/// <summary>Field backing the NumOfCases property</summary>
		private int NumOfCases_field;
		/// <summary>Message to marginal of 'NumOfCases'</summary>
		public PointMass<int> NumOfCases_marginal_F;
		/// <summary>Message to marginal of 'PossibleEvents'</summary>
		public Dirichlet PossibleEvents_marginal_F;
		/// <summary>Message to marginal of 'PossibleRel'</summary>
		public Dirichlet PossibleRel_marginal_F;
		/// <summary>Message to marginal of 'PossibleTraits'</summary>
		public Dirichlet PossibleTraits_marginal_F;
		/// <summary>Field backing the RelStatusPriors property</summary>
		private Dirichlet RelStatusPriors_field;
		/// <summary>Message to marginal of 'RelStatusPriors'</summary>
		public PointMass<Dirichlet> RelStatusPriors_marginal_F;
		/// <summary>Field backing the TraitsPriors property</summary>
		private Dirichlet TraitsPriors_field;
		/// <summary>Message to marginal of 'TraitsPriors'</summary>
		public PointMass<Dirichlet> TraitsPriors_marginal_F;
		#endregion

		#region Properties
		/// <summary>The externally-specified value of 'AllEvents'</summary>
		public int[] AllEvents
		{
			get {
				return this.AllEvents_field;
			}
			set {
				if ((value!=null)&&(value.Length!=this.NumOfCases)) {
					throw new ArgumentException(((("Provided array of length "+value.Length)+" when length ")+this.NumOfCases)+" was expected for variable \'AllEvents\'");
				}
				this.AllEvents_field = value;
				this.numberOfIterationsDone = 0;
				this.Changed_AllEvents_NumOfCases_isDone = false;
				this.Changed_AllEvents_AllRels_AllTraits_DialogueChild_DialogueCPTPrior_NumOfCases_isDone = false;
				this.Changed_AllEvents_EventsPriors_NumOfCases_isDone = false;
			}
		}

		/// <summary>The externally-specified value of 'AllRels'</summary>
		public int[] AllRels
		{
			get {
				return this.AllRels_field;
			}
			set {
				if ((value!=null)&&(value.Length!=this.NumOfCases)) {
					throw new ArgumentException(((("Provided array of length "+value.Length)+" when length ")+this.NumOfCases)+" was expected for variable \'AllRels\'");
				}
				this.AllRels_field = value;
				this.numberOfIterationsDone = 0;
				this.Changed_AllRels_NumOfCases_isDone = false;
				this.Changed_AllEvents_AllRels_AllTraits_DialogueChild_DialogueCPTPrior_NumOfCases_isDone = false;
				this.Changed_AllRels_NumOfCases_RelStatusPriors_isDone = false;
			}
		}

		/// <summary>The externally-specified value of 'AllTraits'</summary>
		public int[] AllTraits
		{
			get {
				return this.AllTraits_field;
			}
			set {
				if ((value!=null)&&(value.Length!=this.NumOfCases)) {
					throw new ArgumentException(((("Provided array of length "+value.Length)+" when length ")+this.NumOfCases)+" was expected for variable \'AllTraits\'");
				}
				this.AllTraits_field = value;
				this.numberOfIterationsDone = 0;
				this.Changed_AllTraits_NumOfCases_isDone = false;
				this.Changed_AllEvents_AllRels_AllTraits_DialogueChild_DialogueCPTPrior_NumOfCases_isDone = false;
				this.Changed_AllTraits_NumOfCases_TraitsPriors_isDone = false;
			}
		}

		/// <summary>The externally-specified value of 'DialogueChild'</summary>
		public int[] DialogueChild
		{
			get {
				return this.DialogueChild_field;
			}
			set {
				if ((value!=null)&&(value.Length!=this.NumOfCases)) {
					throw new ArgumentException(((("Provided array of length "+value.Length)+" when length ")+this.NumOfCases)+" was expected for variable \'DialogueChild\'");
				}
				this.DialogueChild_field = value;
				this.numberOfIterationsDone = 0;
				this.Changed_DialogueChild_NumOfCases_isDone = false;
				this.Changed_AllEvents_AllRels_AllTraits_DialogueChild_DialogueCPTPrior_NumOfCases_isDone = false;
			}
		}

		/// <summary>The externally-specified value of 'DialogueCPTPrior'</summary>
		public Dirichlet[][][] DialogueCPTPrior
		{
			get {
				return this.DialogueCPTPrior_field;
			}
			set {
				if ((value!=null)&&(value.Length!=215)) {
					throw new ArgumentException(((("Provided array of length "+value.Length)+" when length ")+215)+" was expected for variable \'DialogueCPTPrior\'");
				}
				this.DialogueCPTPrior_field = value;
				this.numberOfIterationsDone = 0;
				this.Changed_DialogueCPTPrior_isDone = false;
				this.Changed_AllEvents_AllRels_AllTraits_DialogueChild_DialogueCPTPrior_NumOfCases_isDone = false;
			}
		}

		/// <summary>The externally-specified value of 'EventsPriors'</summary>
		public Dirichlet EventsPriors
		{
			get {
				return this.EventsPriors_field;
			}
			set {
				this.EventsPriors_field = value;
				this.numberOfIterationsDone = 0;
				this.Changed_EventsPriors_isDone = false;
				this.Changed_AllEvents_EventsPriors_NumOfCases_isDone = false;
			}
		}

		/// <summary>The number of iterations done from the initial state</summary>
		public int NumberOfIterationsDone
		{
			get {
				return this.numberOfIterationsDone;
			}
		}

		/// <summary>The externally-specified value of 'NumOfCases'</summary>
		public int NumOfCases
		{
			get {
				return this.NumOfCases_field;
			}
			set {
				if (this.NumOfCases_field!=value) {
					this.NumOfCases_field = value;
					this.numberOfIterationsDone = 0;
					this.Changed_DialogueChild_NumOfCases_isDone = false;
					this.Changed_AllRels_NumOfCases_isDone = false;
					this.Changed_AllTraits_NumOfCases_isDone = false;
					this.Changed_AllEvents_NumOfCases_isDone = false;
					this.Changed_NumOfCases_isDone = false;
					this.Changed_AllEvents_AllRels_AllTraits_DialogueChild_DialogueCPTPrior_NumOfCases_isDone = false;
					this.Changed_AllRels_NumOfCases_RelStatusPriors_isDone = false;
					this.Changed_AllTraits_NumOfCases_TraitsPriors_isDone = false;
					this.Changed_AllEvents_EventsPriors_NumOfCases_isDone = false;
				}
			}
		}

		/// <summary>The externally-specified value of 'RelStatusPriors'</summary>
		public Dirichlet RelStatusPriors
		{
			get {
				return this.RelStatusPriors_field;
			}
			set {
				this.RelStatusPriors_field = value;
				this.numberOfIterationsDone = 0;
				this.Changed_RelStatusPriors_isDone = false;
				this.Changed_AllRels_NumOfCases_RelStatusPriors_isDone = false;
			}
		}

		/// <summary>The externally-specified value of 'TraitsPriors'</summary>
		public Dirichlet TraitsPriors
		{
			get {
				return this.TraitsPriors_field;
			}
			set {
				this.TraitsPriors_field = value;
				this.numberOfIterationsDone = 0;
				this.Changed_TraitsPriors_isDone = false;
				this.Changed_AllTraits_NumOfCases_TraitsPriors_isDone = false;
			}
		}

		#endregion

		#region Methods
		/// <summary>
		/// Returns the marginal distribution for 'AllEvents' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public DistributionRefArray<Discrete,int> AllEventsMarginal()
		{
			return this.AllEvents_marginal_F;
		}

		/// <summary>
		/// Returns the marginal distribution for 'AllRels' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public DistributionRefArray<Discrete,int> AllRelsMarginal()
		{
			return this.AllRels_marginal_F;
		}

		/// <summary>
		/// Returns the marginal distribution for 'AllTraits' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public DistributionRefArray<Discrete,int> AllTraitsMarginal()
		{
			return this.AllTraits_marginal_F;
		}

		/// <summary>Computations that depend on the observed value of AllEvents and AllRels and AllTraits and DialogueChild and DialogueCPTPrior and NumOfCases</summary>
		private void Changed_AllEvents_AllRels_AllTraits_DialogueChild_DialogueCPTPrior_NumOfCases()
		{
			if (this.Changed_AllEvents_AllRels_AllTraits_DialogueChild_DialogueCPTPrior_NumOfCases_isDone) {
				return ;
			}
			// Create array for 'DialogueCPT_marginal' Forwards messages.
			this.DialogueCPT_marginal_F = new DistributionRefArray<DistributionRefArray<DistributionRefArray<Dirichlet,Vector>,Vector[]>,Vector[][]>(215);
			DistributionRefArray<DistributionRefArray<DistributionRefArray<Dirichlet,Vector>,Vector[]>,Vector[][]> DialogueCPT_use_B;
			// Create array for 'DialogueCPT_use' Backwards messages.
			DialogueCPT_use_B = new DistributionRefArray<DistributionRefArray<DistributionRefArray<Dirichlet,Vector>,Vector[]>,Vector[][]>(215);
			for(int Events = 0; Events<215; Events++) {
				// Create array for 'DialogueCPT_use' Backwards messages.
				DialogueCPT_use_B[Events] = new DistributionRefArray<DistributionRefArray<Dirichlet,Vector>,Vector[]>(14);
				for(int Traits = 0; Traits<14; Traits++) {
					// Create array for 'DialogueCPT_use' Backwards messages.
					DialogueCPT_use_B[Events][Traits] = new DistributionRefArray<Dirichlet,Vector>(3);
					for(int Rel = 0; Rel<3; Rel++) {
						DialogueCPT_use_B[Events][Traits][Rel] = ArrayHelper.MakeUniform<Dirichlet>(this.DialogueCPTPrior[Events][Traits][Rel]);
					}
				}
			}
			DistributionRefArray<Dirichlet,Vector> DialogueCPT_itemAllEvents_NCases_AllTraits_NCases_AllRels_NCases__B;
			// Create array for 'DialogueCPT_itemAllEvents_NCases_AllTraits_NCases_AllRels_NCases_' Backwards messages.
			DialogueCPT_itemAllEvents_NCases_AllTraits_NCases_AllRels_NCases__B = new DistributionRefArray<Dirichlet,Vector>(this.NumOfCases);
			for(int NCases = 0; NCases<this.NumOfCases; NCases++) {
				DialogueCPT_itemAllEvents_NCases_AllTraits_NCases_AllRels_NCases__B[NCases] = ArrayHelper.MakeUniform<Dirichlet>(this.DialogueCPTPrior[this.AllEvents[NCases]][this.AllTraits[NCases]][this.AllRels[NCases]]);
				// Message to 'DialogueCPT_itemAllEvents_NCases_AllTraits_NCases_AllRels_NCases_' from Discrete factor
				DialogueCPT_itemAllEvents_NCases_AllTraits_NCases_AllRels_NCases__B[NCases] = DiscreteFromDirichletOp.ProbsAverageConditional(this.DialogueChild[NCases], DialogueCPT_itemAllEvents_NCases_AllTraits_NCases_AllRels_NCases__B[NCases]);
			}
			// Message to 'DialogueCPT_use' from GetItemsFromDeepJagged factor
			DialogueCPT_use_B = GetItemsFromDeepJaggedOp<Vector>.ArrayAverageConditional<DistributionRefArray<DistributionRefArray<DistributionRefArray<Dirichlet,Vector>,Vector[]>,Vector[][]>,DistributionRefArray<DistributionRefArray<Dirichlet,Vector>,Vector[]>,DistributionRefArray<Dirichlet,Vector>,Dirichlet>(DialogueCPT_itemAllEvents_NCases_AllTraits_NCases_AllRels_NCases__B, this.AllEvents, this.AllTraits, this.AllRels, DialogueCPT_use_B);
			for(int Events = 0; Events<215; Events++) {
				// Create array for 'DialogueCPT_marginal' Forwards messages.
				this.DialogueCPT_marginal_F[Events] = new DistributionRefArray<DistributionRefArray<Dirichlet,Vector>,Vector[]>(14);
				for(int Traits = 0; Traits<14; Traits++) {
					// Create array for 'DialogueCPT_marginal' Forwards messages.
					this.DialogueCPT_marginal_F[Events][Traits] = new DistributionRefArray<Dirichlet,Vector>(3);
					for(int Rel = 0; Rel<3; Rel++) {
						this.DialogueCPT_marginal_F[Events][Traits][Rel] = ArrayHelper.MakeUniform<Dirichlet>(this.DialogueCPTPrior[Events][Traits][Rel]);
						// Message to 'DialogueCPT_marginal' from Variable factor
						this.DialogueCPT_marginal_F[Events][Traits][Rel] = VariableOp.MarginalAverageConditional<Dirichlet>(DialogueCPT_use_B[Events][Traits][Rel], this.DialogueCPTPrior[Events][Traits][Rel], this.DialogueCPT_marginal_F[Events][Traits][Rel]);
					}
				}
			}
			this.Changed_AllEvents_AllRels_AllTraits_DialogueChild_DialogueCPTPrior_NumOfCases_isDone = true;
		}

		/// <summary>Computations that depend on the observed value of AllEvents and EventsPriors and NumOfCases</summary>
		private void Changed_AllEvents_EventsPriors_NumOfCases()
		{
			if (this.Changed_AllEvents_EventsPriors_NumOfCases_isDone) {
				return ;
			}
			this.PossibleEvents_marginal_F = ArrayHelper.MakeUniform<Dirichlet>(this.EventsPriors);
			Dirichlet[] PossibleEvents_rep_B;
			// Create array for 'PossibleEvents_rep' Backwards messages.
			PossibleEvents_rep_B = new Dirichlet[this.NumOfCases];
			for(int NCases = 0; NCases<this.NumOfCases; NCases++) {
				PossibleEvents_rep_B[NCases] = ArrayHelper.MakeUniform<Dirichlet>(this.EventsPriors);
				// Message to 'PossibleEvents_rep' from Discrete factor
				PossibleEvents_rep_B[NCases] = DiscreteFromDirichletOp.ProbsAverageConditional(this.AllEvents[NCases], PossibleEvents_rep_B[NCases]);
			}
			Dirichlet PossibleEvents_rep_B_toDef;
			PossibleEvents_rep_B_toDef = ReplicateOp_Divide.ToDefInit<Dirichlet>(this.EventsPriors);
			PossibleEvents_rep_B_toDef = ReplicateOp_Divide.ToDef<Dirichlet>(PossibleEvents_rep_B, PossibleEvents_rep_B_toDef);
			// Message to 'PossibleEvents_marginal' from Variable factor
			this.PossibleEvents_marginal_F = VariableOp.MarginalAverageConditional<Dirichlet>(PossibleEvents_rep_B_toDef, this.EventsPriors, this.PossibleEvents_marginal_F);
			this.Changed_AllEvents_EventsPriors_NumOfCases_isDone = true;
		}

		/// <summary>Computations that depend on the observed value of AllEvents and NumOfCases</summary>
		private void Changed_AllEvents_NumOfCases()
		{
			if (this.Changed_AllEvents_NumOfCases_isDone) {
				return ;
			}
			// Create array for 'AllEvents_marginal' Forwards messages.
			this.AllEvents_marginal_F = new DistributionRefArray<Discrete,int>(this.NumOfCases);
			for(int NCases = 0; NCases<this.NumOfCases; NCases++) {
				this.AllEvents_marginal_F[NCases] = ArrayHelper.MakeUniform<Discrete>(Discrete.Uniform(215));
			}
			// Message to 'AllEvents_marginal' from DerivedVariable factor
			this.AllEvents_marginal_F = DerivedVariableOp.MarginalAverageConditional<DistributionRefArray<Discrete,int>,int[]>(this.AllEvents, this.AllEvents_marginal_F);
			this.Changed_AllEvents_NumOfCases_isDone = true;
		}

		/// <summary>Computations that depend on the observed value of AllRels and NumOfCases</summary>
		private void Changed_AllRels_NumOfCases()
		{
			if (this.Changed_AllRels_NumOfCases_isDone) {
				return ;
			}
			// Create array for 'AllRels_marginal' Forwards messages.
			this.AllRels_marginal_F = new DistributionRefArray<Discrete,int>(this.NumOfCases);
			for(int NCases = 0; NCases<this.NumOfCases; NCases++) {
				this.AllRels_marginal_F[NCases] = ArrayHelper.MakeUniform<Discrete>(Discrete.Uniform(3));
			}
			// Message to 'AllRels_marginal' from DerivedVariable factor
			this.AllRels_marginal_F = DerivedVariableOp.MarginalAverageConditional<DistributionRefArray<Discrete,int>,int[]>(this.AllRels, this.AllRels_marginal_F);
			this.Changed_AllRels_NumOfCases_isDone = true;
		}

		/// <summary>Computations that depend on the observed value of AllRels and NumOfCases and RelStatusPriors</summary>
		private void Changed_AllRels_NumOfCases_RelStatusPriors()
		{
			if (this.Changed_AllRels_NumOfCases_RelStatusPriors_isDone) {
				return ;
			}
			this.PossibleRel_marginal_F = ArrayHelper.MakeUniform<Dirichlet>(this.RelStatusPriors);
			Dirichlet[] PossibleRel_rep_B;
			// Create array for 'PossibleRel_rep' Backwards messages.
			PossibleRel_rep_B = new Dirichlet[this.NumOfCases];
			for(int NCases = 0; NCases<this.NumOfCases; NCases++) {
				PossibleRel_rep_B[NCases] = ArrayHelper.MakeUniform<Dirichlet>(this.RelStatusPriors);
				// Message to 'PossibleRel_rep' from Discrete factor
				PossibleRel_rep_B[NCases] = DiscreteFromDirichletOp.ProbsAverageConditional(this.AllRels[NCases], PossibleRel_rep_B[NCases]);
			}
			Dirichlet PossibleRel_rep_B_toDef;
			PossibleRel_rep_B_toDef = ReplicateOp_Divide.ToDefInit<Dirichlet>(this.RelStatusPriors);
			PossibleRel_rep_B_toDef = ReplicateOp_Divide.ToDef<Dirichlet>(PossibleRel_rep_B, PossibleRel_rep_B_toDef);
			// Message to 'PossibleRel_marginal' from Variable factor
			this.PossibleRel_marginal_F = VariableOp.MarginalAverageConditional<Dirichlet>(PossibleRel_rep_B_toDef, this.RelStatusPriors, this.PossibleRel_marginal_F);
			this.Changed_AllRels_NumOfCases_RelStatusPriors_isDone = true;
		}

		/// <summary>Computations that depend on the observed value of AllTraits and NumOfCases</summary>
		private void Changed_AllTraits_NumOfCases()
		{
			if (this.Changed_AllTraits_NumOfCases_isDone) {
				return ;
			}
			// Create array for 'AllTraits_marginal' Forwards messages.
			this.AllTraits_marginal_F = new DistributionRefArray<Discrete,int>(this.NumOfCases);
			for(int NCases = 0; NCases<this.NumOfCases; NCases++) {
				this.AllTraits_marginal_F[NCases] = ArrayHelper.MakeUniform<Discrete>(Discrete.Uniform(14));
			}
			// Message to 'AllTraits_marginal' from DerivedVariable factor
			this.AllTraits_marginal_F = DerivedVariableOp.MarginalAverageConditional<DistributionRefArray<Discrete,int>,int[]>(this.AllTraits, this.AllTraits_marginal_F);
			this.Changed_AllTraits_NumOfCases_isDone = true;
		}

		/// <summary>Computations that depend on the observed value of AllTraits and NumOfCases and TraitsPriors</summary>
		private void Changed_AllTraits_NumOfCases_TraitsPriors()
		{
			if (this.Changed_AllTraits_NumOfCases_TraitsPriors_isDone) {
				return ;
			}
			this.PossibleTraits_marginal_F = ArrayHelper.MakeUniform<Dirichlet>(this.TraitsPriors);
			Dirichlet[] PossibleTraits_rep_B;
			// Create array for 'PossibleTraits_rep' Backwards messages.
			PossibleTraits_rep_B = new Dirichlet[this.NumOfCases];
			for(int NCases = 0; NCases<this.NumOfCases; NCases++) {
				PossibleTraits_rep_B[NCases] = ArrayHelper.MakeUniform<Dirichlet>(this.TraitsPriors);
				// Message to 'PossibleTraits_rep' from Discrete factor
				PossibleTraits_rep_B[NCases] = DiscreteFromDirichletOp.ProbsAverageConditional(this.AllTraits[NCases], PossibleTraits_rep_B[NCases]);
			}
			Dirichlet PossibleTraits_rep_B_toDef;
			PossibleTraits_rep_B_toDef = ReplicateOp_Divide.ToDefInit<Dirichlet>(this.TraitsPriors);
			PossibleTraits_rep_B_toDef = ReplicateOp_Divide.ToDef<Dirichlet>(PossibleTraits_rep_B, PossibleTraits_rep_B_toDef);
			// Message to 'PossibleTraits_marginal' from Variable factor
			this.PossibleTraits_marginal_F = VariableOp.MarginalAverageConditional<Dirichlet>(PossibleTraits_rep_B_toDef, this.TraitsPriors, this.PossibleTraits_marginal_F);
			this.Changed_AllTraits_NumOfCases_TraitsPriors_isDone = true;
		}

		/// <summary>Computations that depend on the observed value of DialogueChild and NumOfCases</summary>
		private void Changed_DialogueChild_NumOfCases()
		{
			if (this.Changed_DialogueChild_NumOfCases_isDone) {
				return ;
			}
			// Create array for 'DialogueChild_marginal' Forwards messages.
			this.DialogueChild_marginal_F = new DistributionRefArray<Discrete,int>(this.NumOfCases);
			for(int NCases = 0; NCases<this.NumOfCases; NCases++) {
				this.DialogueChild_marginal_F[NCases] = ArrayHelper.MakeUniform<Discrete>(Discrete.Uniform(670));
			}
			// Message to 'DialogueChild_marginal' from DerivedVariable factor
			this.DialogueChild_marginal_F = DerivedVariableOp.MarginalAverageConditional<DistributionRefArray<Discrete,int>,int[]>(this.DialogueChild, this.DialogueChild_marginal_F);
			this.Changed_DialogueChild_NumOfCases_isDone = true;
		}

		/// <summary>Computations that depend on the observed value of DialogueCPTPrior</summary>
		private void Changed_DialogueCPTPrior()
		{
			if (this.Changed_DialogueCPTPrior_isDone) {
				return ;
			}
			// Create array for 'DialogueCPTPrior_marginal' Forwards messages.
			this.DialogueCPTPrior_marginal_F = new PointMass<Dirichlet[][][]>(this.DialogueCPTPrior);
			// Message to 'DialogueCPTPrior_marginal' from DerivedVariable factor
			this.DialogueCPTPrior_marginal_F = DerivedVariableOp.MarginalAverageConditional<PointMass<Dirichlet[][][]>,Dirichlet[][][]>(this.DialogueCPTPrior, this.DialogueCPTPrior_marginal_F);
			this.Changed_DialogueCPTPrior_isDone = true;
		}

		/// <summary>Computations that depend on the observed value of EventsPriors</summary>
		private void Changed_EventsPriors()
		{
			if (this.Changed_EventsPriors_isDone) {
				return ;
			}
			this.EventsPriors_marginal_F = new PointMass<Dirichlet>(this.EventsPriors);
			// Message to 'EventsPriors_marginal' from DerivedVariable factor
			this.EventsPriors_marginal_F = DerivedVariableOp.MarginalAverageConditional<PointMass<Dirichlet>,Dirichlet>(this.EventsPriors, this.EventsPriors_marginal_F);
			this.Changed_EventsPriors_isDone = true;
		}

		/// <summary>Computations that depend on the observed value of NumOfCases</summary>
		private void Changed_NumOfCases()
		{
			if (this.Changed_NumOfCases_isDone) {
				return ;
			}
			this.NumOfCases_marginal_F = new PointMass<int>(this.NumOfCases);
			// Message to 'NumOfCases_marginal' from DerivedVariable factor
			this.NumOfCases_marginal_F = DerivedVariableOp.MarginalAverageConditional<PointMass<int>,int>(this.NumOfCases, this.NumOfCases_marginal_F);
			this.Changed_NumOfCases_isDone = true;
		}

		/// <summary>Computations that depend on the observed value of RelStatusPriors</summary>
		private void Changed_RelStatusPriors()
		{
			if (this.Changed_RelStatusPriors_isDone) {
				return ;
			}
			this.RelStatusPriors_marginal_F = new PointMass<Dirichlet>(this.RelStatusPriors);
			// Message to 'RelStatusPriors_marginal' from DerivedVariable factor
			this.RelStatusPriors_marginal_F = DerivedVariableOp.MarginalAverageConditional<PointMass<Dirichlet>,Dirichlet>(this.RelStatusPriors, this.RelStatusPriors_marginal_F);
			this.Changed_RelStatusPriors_isDone = true;
		}

		/// <summary>Computations that depend on the observed value of TraitsPriors</summary>
		private void Changed_TraitsPriors()
		{
			if (this.Changed_TraitsPriors_isDone) {
				return ;
			}
			this.TraitsPriors_marginal_F = new PointMass<Dirichlet>(this.TraitsPriors);
			// Message to 'TraitsPriors_marginal' from DerivedVariable factor
			this.TraitsPriors_marginal_F = DerivedVariableOp.MarginalAverageConditional<PointMass<Dirichlet>,Dirichlet>(this.TraitsPriors, this.TraitsPriors_marginal_F);
			this.Changed_TraitsPriors_isDone = true;
		}

		/// <summary>
		/// Returns the marginal distribution for 'DialogueChild' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public DistributionRefArray<Discrete,int> DialogueChildMarginal()
		{
			return this.DialogueChild_marginal_F;
		}

		/// <summary>
		/// Returns the marginal distribution for 'DialogueCPT' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public DistributionRefArray<DistributionRefArray<DistributionRefArray<Dirichlet,Vector>,Vector[]>,Vector[][]> DialogueCPTMarginal()
		{
			return this.DialogueCPT_marginal_F;
		}

		/// <summary>
		/// Returns the marginal distribution for 'DialogueCPTPrior' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public PointMass<Dirichlet[][][]> DialogueCPTPriorMarginal()
		{
			return this.DialogueCPTPrior_marginal_F;
		}

		/// <summary>
		/// Returns the marginal distribution for 'EventsPriors' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public PointMass<Dirichlet> EventsPriorsMarginal()
		{
			return this.EventsPriors_marginal_F;
		}

		/// <summary>Update all marginals, by iterating message passing the given number of times</summary>
		/// <param name="numberOfIterations">The number of times to iterate each loop</param>
		/// <param name="initialise">If true, messages that initialise loops are reset when observed values change</param>
		private void Execute(int numberOfIterations, bool initialise)
		{
			this.Changed_DialogueChild_NumOfCases();
			this.Changed_AllRels_NumOfCases();
			this.Changed_AllTraits_NumOfCases();
			this.Changed_AllEvents_NumOfCases();
			this.Changed_DialogueCPTPrior();
			this.Changed_RelStatusPriors();
			this.Changed_TraitsPriors();
			this.Changed_EventsPriors();
			this.Changed_NumOfCases();
			this.Changed_AllEvents_AllRels_AllTraits_DialogueChild_DialogueCPTPrior_NumOfCases();
			this.Changed_AllRels_NumOfCases_RelStatusPriors();
			this.Changed_AllTraits_NumOfCases_TraitsPriors();
			this.Changed_AllEvents_EventsPriors_NumOfCases();
			this.numberOfIterationsDone = numberOfIterations;
		}

		/// <summary>Update all marginals, by iterating message-passing the given number of times</summary>
		/// <param name="numberOfIterations">The total number of iterations that should be executed for the current set of observed values.  If this is more than the number already done, only the extra iterations are done.  If this is less than the number already done, message-passing is restarted from the beginning.  Changing the observed values resets the iteration count to 0.</param>
		public void Execute(int numberOfIterations)
		{
			this.Execute(numberOfIterations, true);
		}

		/// <summary>Get the observed value of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		public object GetObservedValue(string variableName)
		{
			if (variableName=="NumOfCases") {
				return this.NumOfCases;
			}
			if (variableName=="EventsPriors") {
				return this.EventsPriors;
			}
			if (variableName=="TraitsPriors") {
				return this.TraitsPriors;
			}
			if (variableName=="RelStatusPriors") {
				return this.RelStatusPriors;
			}
			if (variableName=="DialogueCPTPrior") {
				return this.DialogueCPTPrior;
			}
			if (variableName=="AllEvents") {
				return this.AllEvents;
			}
			if (variableName=="AllTraits") {
				return this.AllTraits;
			}
			if (variableName=="AllRels") {
				return this.AllRels;
			}
			if (variableName=="DialogueChild") {
				return this.DialogueChild;
			}
			throw new ArgumentException("Not an observed variable name: "+variableName);
		}

		/// <summary>Get the marginal distribution (computed up to this point) of a variable</summary>
		/// <param name="variableName">Name of the variable in the generated code</param>
		/// <returns>The marginal distribution computed up to this point</returns>
		/// <remarks>Execute, Update, or Reset must be called first to set the value of the marginal.</remarks>
		public object Marginal(string variableName)
		{
			if (variableName=="DialogueChild") {
				return this.DialogueChildMarginal();
			}
			if (variableName=="AllRels") {
				return this.AllRelsMarginal();
			}
			if (variableName=="AllTraits") {
				return this.AllTraitsMarginal();
			}
			if (variableName=="AllEvents") {
				return this.AllEventsMarginal();
			}
			if (variableName=="DialogueCPTPrior") {
				return this.DialogueCPTPriorMarginal();
			}
			if (variableName=="RelStatusPriors") {
				return this.RelStatusPriorsMarginal();
			}
			if (variableName=="TraitsPriors") {
				return this.TraitsPriorsMarginal();
			}
			if (variableName=="EventsPriors") {
				return this.EventsPriorsMarginal();
			}
			if (variableName=="NumOfCases") {
				return this.NumOfCasesMarginal();
			}
			if (variableName=="DialogueCPT") {
				return this.DialogueCPTMarginal();
			}
			if (variableName=="PossibleRel") {
				return this.PossibleRelMarginal();
			}
			if (variableName=="PossibleTraits") {
				return this.PossibleTraitsMarginal();
			}
			if (variableName=="PossibleEvents") {
				return this.PossibleEventsMarginal();
			}
			throw new ArgumentException("This class was not built to infer "+variableName);
		}

		/// <summary>Get the marginal distribution (computed up to this point) of a variable, converted to type T</summary>
		/// <typeparam name="T">The distribution type.</typeparam>
		/// <param name="variableName">Name of the variable in the generated code</param>
		/// <returns>The marginal distribution computed up to this point</returns>
		/// <remarks>Execute, Update, or Reset must be called first to set the value of the marginal.</remarks>
		public T Marginal<T>(string variableName)
		{
			return Distribution.ChangeType<T>(this.Marginal(variableName));
		}

		/// <summary>Get the query-specific marginal distribution of a variable.</summary>
		/// <param name="variableName">Name of the variable in the generated code</param>
		/// <param name="query">QueryType name. For example, GibbsSampling answers 'Marginal', 'Samples', and 'Conditionals' queries</param>
		/// <remarks>Execute, Update, or Reset must be called first to set the value of the marginal.</remarks>
		public object Marginal(string variableName, string query)
		{
			if (query=="Marginal") {
				return this.Marginal(variableName);
			}
			throw new ArgumentException(((("This class was not built to infer \'"+variableName)+"\' with query \'")+query)+"\'");
		}

		/// <summary>Get the query-specific marginal distribution of a variable, converted to type T</summary>
		/// <typeparam name="T">The distribution type.</typeparam>
		/// <param name="variableName">Name of the variable in the generated code</param>
		/// <param name="query">QueryType name. For example, GibbsSampling answers 'Marginal', 'Samples', and 'Conditionals' queries</param>
		/// <remarks>Execute, Update, or Reset must be called first to set the value of the marginal.</remarks>
		public T Marginal<T>(string variableName, string query)
		{
			return Distribution.ChangeType<T>(this.Marginal(variableName, query));
		}

		/// <summary>
		/// Returns the marginal distribution for 'NumOfCases' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public PointMass<int> NumOfCasesMarginal()
		{
			return this.NumOfCases_marginal_F;
		}

		private void OnProgressChanged(ProgressChangedEventArgs e)
		{
			// Make a temporary copy of the event to avoid a race condition
			// if the last subscriber unsubscribes immediately after the null check and before the event is raised.
			EventHandler<ProgressChangedEventArgs> handler = this.ProgressChanged;
			if (handler!=null) {
				handler(this, e);
			}
		}

		/// <summary>
		/// Returns the marginal distribution for 'PossibleEvents' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public Dirichlet PossibleEventsMarginal()
		{
			return this.PossibleEvents_marginal_F;
		}

		/// <summary>
		/// Returns the marginal distribution for 'PossibleRel' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public Dirichlet PossibleRelMarginal()
		{
			return this.PossibleRel_marginal_F;
		}

		/// <summary>
		/// Returns the marginal distribution for 'PossibleTraits' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public Dirichlet PossibleTraitsMarginal()
		{
			return this.PossibleTraits_marginal_F;
		}

		/// <summary>
		/// Returns the marginal distribution for 'RelStatusPriors' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public PointMass<Dirichlet> RelStatusPriorsMarginal()
		{
			return this.RelStatusPriors_marginal_F;
		}

		/// <summary>Reset all messages to their initial values.  Sets NumberOfIterationsDone to 0.</summary>
		public void Reset()
		{
			this.Execute(0);
		}

		/// <summary>Set the observed value of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Observed value</param>
		public void SetObservedValue(string variableName, object value)
		{
			if (variableName=="NumOfCases") {
				this.NumOfCases = (int)value;
				return ;
			}
			if (variableName=="EventsPriors") {
				this.EventsPriors = (Dirichlet)value;
				return ;
			}
			if (variableName=="TraitsPriors") {
				this.TraitsPriors = (Dirichlet)value;
				return ;
			}
			if (variableName=="RelStatusPriors") {
				this.RelStatusPriors = (Dirichlet)value;
				return ;
			}
			if (variableName=="DialogueCPTPrior") {
				this.DialogueCPTPrior = (Dirichlet[][][])value;
				return ;
			}
			if (variableName=="AllEvents") {
				this.AllEvents = (int[])value;
				return ;
			}
			if (variableName=="AllTraits") {
				this.AllTraits = (int[])value;
				return ;
			}
			if (variableName=="AllRels") {
				this.AllRels = (int[])value;
				return ;
			}
			if (variableName=="DialogueChild") {
				this.DialogueChild = (int[])value;
				return ;
			}
			throw new ArgumentException("Not an observed variable name: "+variableName);
		}

		/// <summary>
		/// Returns the marginal distribution for 'TraitsPriors' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public PointMass<Dirichlet> TraitsPriorsMarginal()
		{
			return this.TraitsPriors_marginal_F;
		}

		/// <summary>Update all marginals, by iterating message-passing an additional number of times</summary>
		/// <param name="additionalIterations">The number of iterations that should be executed, starting from the current message state.  Messages are not reset, even if observed values have changed.</param>
		public void Update(int additionalIterations)
		{
			this.Execute(checked(this.numberOfIterationsDone+additionalIterations), false);
		}

		#endregion

		#region Events
		/// <summary>Event that is fired when the progress of inference changes, typically at the end of one iteration of the inference algorithm.</summary>
		public event EventHandler<ProgressChangedEventArgs> ProgressChanged;
		#endregion

	}

}
