using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepShi.Helpers
{
    public class ApiDictionary
    {
        #region User

        public const string UserApiAuthenticateUser = "Api/Account/Login";
        public const string UserApiLogout = "Api/Account/Logout";
        public const string UserApiRegisterUser = "Api/Account/Register";
        public const string UserApiValidateUserInfo = "Api/User/Account";
        public const string UserApiUpdateUserPassword = "Api/User/Account";
        public const string UserApiEmailExistsOrNot = "Api/Account/IsEmailExists";
        public const string UserApiConfirmEmail = "Api/Account/ConfirmEmail";
        public const string UserApiForgetPassword = "Api/Account/Forgetpassword";
        public const string UserApiResetPassword = "Api/Account/ResetPassword";
        public const string UserApiChangePassword = "Api/Account/ChangePassword";

        public const string UserApiUserList = "Api/Account/UserList";
        #endregion

        #region Client

        public const string ClientApiSaveClient = "Api/Client/SaveClient";
        public const string ClientApiUpdateClient = "Api/Client/UpdateClient";
        public const string ClientApiGetClient = "Api/Client/GetClient";
        public const string ClientApiGetClientList = "Api/Client/GetClientList";

        #endregion

        #region Grade

        public const string GradeApiGetGrade = "Api/Grade/GetGrade";
        public const string GradeApiSaveGrade = "Api/Grade/SaveGrade";
        public const string GradeApiUpdateGrade = "Api/Grade/UpdateGrade";
        public const string GradeApiGetGradeList = "Api/Grade/GetGradeList";

        #endregion

        #region Post       
        public const string PostApiSavePost = "Api/Post/SavePost";
        public const string PostApiPostList = "Api/Post/GetPostList";
        public const string PostApiGetPost = "Api/Post/GetPost";
        public const string PostApiUpdatePost = "Api/Post/UpdatePost";

        public const string PostApiAllOrgThatTagWithPosts = "Api/Post/AllOrgThatTagWithPosts";
        public const string PostApiAllPostsByOrg = "Api/Post/AllPostsByOrg";
        public const string PostApiAllPostsByDocId = "Api/Post/AllPostsByDocId";

        #endregion

        #region Cadre And CRCodes
        public const string CRCodeApiCRCodeList = "Api/CRCode/CRCode";
        public const string CadreApiCadreList = "Api/Cadre/CadreList";
        #endregion

        #region Specialisation       
        public const string SpecialisationApiSaveSpecialisation = "Api/Specialisation/SaveSpecialisation";
        public const string SpecialisationApiSpecialisationList = "Api/Specialisation/GetSpecialisationList";
        public const string SpecialisationApiGetSpecialisation = "Api/Specialisation/GetSpecialisation";
        public const string SpecialisationApiUpdateSpecialisation = "Api/Specialisation/UpdateSpecialisation";

        #endregion

        #region Type Of Test Master

        public const string TypeOfTestMasterApiSave = "Api/TypeOfTest/SaveTypeOfTest";
        public const string TypeOfTestMasterApiUpdate = "Api/TypeOfTest/UpdateTypeOfTest";
        public const string TypeOfTestMasterApiGet = "Api/TypeOfTest/GetTypeOfTest";
        public const string TypeOfTestMasterApiGetList = "Api/TypeOfTest/GetTypeOfTestList";

        #endregion

        #region Additional Services Master

        public const string AdditionalServicesMasterApiSave = "Api/AdditionalServices/SaveAdditionalServices";
        public const string AdditionalServicesMasterApiUpdate = "Api/AdditionalServices/UpdateAdditionalServices";
        public const string AdditionalServicesMasterApiGet = "Api/AdditionalServices/GetAdditionalServices";
        public const string AdditionalServicesMasterApiGetList = "Api/AdditionalServices/GetAdditionalServicesList";

        #endregion

        #region Dispatch Mode Master

        public const string DispatchModeMasterApiSave = "Api/DispatchMode/SaveDispatchMode";
        public const string DispatchModeMasterApiUpdate = "Api/DispatchMode/UpdateDispatchMode";
        public const string DispatchModeMasterApiGet = "Api/DispatchMode/GetDispatchMode";
        public const string DispatchModeMasterApiGetList = "Api/DispatchMode/GetDispatchModeList";

        #endregion

        #region Exam Types Master

        public const string ExamTypesMasterApiSave = "Api/ExamTypes/SaveExamTypes";
        public const string ExamTypesMasterApiUpdate = "Api/ExamTypes/UpdateExamTypes";
        public const string ExamTypesMasterApiGet = "Api/ExamTypes/GetExamTypes";
        public const string ExamTypesMasterApiGetList = "Api/ExamTypes/GetExamTypesList";

        #endregion

        #region Project Handled By Master

        public const string ProjectHandledByMasterApiSave = "Api/ProjectHandledBy/SaveProjectHandledBy";
        public const string ProjectHandledByMasterApiUpdate = "Api/ProjectHandledBy/UpdateProjectHandledBy";
        public const string ProjectHandledByMasterApiGet = "Api/ProjectHandledBy/GetProjectHandledBy";
        public const string ProjectHandledByMasterApiGetList = "Api/ProjectHandledBy/GetProjectHandledByList";

        #endregion

        #region Roll No Structure Master

        public const string RollNoStructureMasterApiSave = "Api/RollNoStructure/SaveRollNoStructure";
        public const string RollNoStructureMasterApiUpdate = "Api/RollNoStructure/UpdateRollNoStructure";
        public const string RollNoStructureMasterApiGet = "Api/RollNoStructure/GetRollNoStructure";
        public const string RollNoStructureMasterApiGetList = "Api/RollNoStructure/GetRollNoStructureList";

        #endregion

        #region Time Table Master

        public const string TimeTableMasterApiSave = "Api/TimeTable/SaveTimeTable";
        public const string TimeTableMasterApiUpdate = "Api/TimeTable/UpdateTimeTable";
        public const string TimeTableMasterApiGet = "Api/TimeTable/GetTimeTable";
        public const string TimeTableMasterApiGetList = "Api/TimeTable/GetTimeTableList";

        #endregion

        #region Project Details
        public const string ProjectDetailsApiSave = "Api/ProjectDetails/SaveProjectDetials";
        public const string ProjectDetailsApiUpdate = "Api/ProjectDetails/UpdateProjectDetials";
        public const string ProjectDetailsApiGet = "Api/ProjectDetails/GetProjectbyDocumentId";
        public const string ProjectDetailsApiGetList = "Api/ProjectDetails/GetProjectDetialsList";

        public const string ProjectDetailsApiGenerateProjectNo = "Api/ProjectDetails/GenerateProjectNo";
        public const string ProjectDetailsApiUpdateHistoryList = "Api/ProjectDetails/GetProjectHistoryList";

        public const string ProjectDetailsApiPdf = "Api/ProjectDetails/GetDataForPDF";
        public const string CrystalReportPdf = "http://localhost:44320/api/GetDataForPDF";

        public const string ProjectDetailsApiPostSplForTestStructure = "Api/ProjectDetails/GetProjectPostAndSplList";

        #endregion

        #region Menu Master
        public const string MenuMasterApiSave = "Api/Menu/SaveMenuMaster";
        public const string MenuMasterApiUpdate = "Api/Menu/UpdateMenuMaster";
        public const string MenuMasterApiGet = "Api/Menu/GetMenuMaster";
        public const string MenuMasterApiGetList = "Api/Menu/GetMenuMasterList";

        public const string MenuMasterApiUserWiseMenuList = "Api/Menu/UserWiseMenuItems";
        public const string MenuMasterApiUpdateMenuPermission = "Api/Menu/UpdateMenuPermission";

        #endregion

        #region Dashboard
        public const string DashboardEventList = "Api/EventCalander/GetEventList";
        public const string DashboardEventSave = "Api/EventCalander/SaveEvent";
        public const string DashboardEventUpdate = "Api/EventCalander/UpdateEvent";
        #endregion

        #region TestNameMaster
        public const string TestSubjectNameMasterApiSave = "Api/TestSubjectNameMaster/SaveTestSubjectName";
        public const string TestSubjectNameMasterApiUpdate = "Api/TestSubjectNameMaster/UpdateTestSubjectName";
        public const string TestSubjectNameMasterApiGet = "Api/TestSubjectNameMaster/GetTestSubjectName";
        public const string TestSubjectNameMasterApiGetList = "Api/TestSubjectNameMaster/GetTestSubjectNameList";
        #endregion

        #region MediumOfExamMaster
        public const string MediumOfExamMasterApiSave = "Api/MediumOfExam/SaveMediumOfExam";
        public const string MediumOfExamMasterApiUpdate = "Api/MediumOfExam/UpdateMediumOfExam";
        public const string MediumOfExamMasterApiGet = "Api/MediumOfExam/GetMediumOfExam";
        public const string MediumOfExamMasterApiGetList = "Api/MediumOfExam/GetMediumOfExamList";
        #endregion

        #region ExperienceMaster
        public const string ExperienceMasterApiSave = "Api/ExperienceMaster/SaveExperience";
        public const string ExperienceMasterApiUpdate = "Api/ExperienceMaster/UpdateExperience";
        public const string ExperienceMasterApiGet = "Api/ExperienceMaster/GetExperience";
        public const string ExperienceMasterApiGetList = "Api/ExperienceMaster/GetExperienceList";
        #endregion
        #region MediumOfExamMaster
        public const string QualificationMasterApiSave = "Api/QualificationMaster/SaveQualification";
        public const string QualificationMasterApiUpdate = "Api/QualificationMaster/UpdateQualification";
        public const string QualificationMasterApiGet = "Api/QualificationMaster/GetQualification";
        public const string QualificationMasterApiGetList = "Api/QualificationMaster/GetQualificationList";
        #endregion
    }
}

